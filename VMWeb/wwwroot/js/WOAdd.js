let selectedProducts = [];
let productQuantities = {}; // Object to store available quantities of products

// Load danh sách sản phẩm và số lượng tồn kho từ API khi trang tải
document.addEventListener("DOMContentLoaded", function () {
    // Load danh sách sản phẩm từ API
    fetch('https://localhost:7000/api/Product/getListProducts')
        .then(response => response.json())
        .then(data => {
            const productSelect = document.getElementById("productSelect");
            data.forEach(product => {
                let option = document.createElement("option");
                option.value = product.productId;
                option.textContent = product.productName;
                productSelect.appendChild(option);

                // Lưu số lượng sản phẩm có sẵn từ API Inventory vào object productQuantities
                fetch(`https://localhost:7000/api/Inventory/getQISByProId/${product.productId}`)
                    .then(inventoryResponse => inventoryResponse.json())
                    .then(inventoryData => {
                        // Lưu số lượng tồn kho của sản phẩm vào productQuantities
                        productQuantities[product.productId] = inventoryData.quantityInStock; // Giả sử API trả về 'quantityInStock'
                    })
                    .catch(error => console.error('Error loading inventory data:', error));
            });
        })
        .catch(error => console.error('Error loading products:', error));

    checkFormValidity(); // Kiểm tra ngay khi trang tải
});

function checkFormValidity() {
    const recipientSelect = document.getElementById("recipientSelect");
    const submitBtn = document.getElementById("submitBtn");
    const productTableBody = document.getElementById("productTableBody");

    let isRecipientSelected = recipientSelect.value.trim() !== "";
    let hasProducts = productTableBody.children.length > 0;

    submitBtn.disabled = !(isRecipientSelected && hasProducts);
}

// Khi chọn Recipient, kiểm tra lại điều kiện submit
document.getElementById("recipientSelect").addEventListener("change", checkFormValidity);

function addProduct() {
    const productSelect = document.getElementById("productSelect");
    const selectedOption = productSelect.options[productSelect.selectedIndex];

    if (!selectedOption.value) {
        alert("Please select a product!");
        return;
    }

    // Kiểm tra số lượng tồn kho
    const availableQuantity = productQuantities[selectedOption.value];
    if (availableQuantity === 0) {
        alert("This product is out of stock and cannot be added.");
        return;
    }
    if (!availableQuantity) {
        alert("Product quantity information is not available.");
        return;
    }

    

    if (selectedProducts.includes(selectedOption.value)) {
        alert("This product is already added!");
        return;
    }

    selectedProducts.push(selectedOption.value);

    const tableBody = document.getElementById("productTableBody");
    const rowIndex = selectedProducts.length - 1;
    const row = document.createElement("tr");

    row.innerHTML = `
        <td>${selectedProducts.length}</td>
        <td>
            <input type="hidden" name="WarehouseOutDetailDTOs[${rowIndex}].ProductId" value="${selectedOption.value}" />
            <input type="text" class="form-control" name="WarehouseOutDetailDTOs[${rowIndex}].ProductName" value="${selectedOption.text}" readonly>
        </td>
        <td>
            <input type="number" class="form-control quantity" name="WarehouseOutDetailDTOs[${rowIndex}].QuantityOut" min="1" value="1" 
                oninput="updateTotal(this, ${rowIndex}, ${availableQuantity})" />
            <span> (Available: ${availableQuantity})</span>
        </td>
        <td><input type="number" class="form-control price" name="WarehouseOutDetailDTOs[${rowIndex}].PriceOut" min="0.01" step="0.01" value="0" oninput="updateTotal(this, ${rowIndex})"></td>
        <td class="total-price" id="total-${rowIndex}">$0.00</td>
        <td><button type="button" class="btn btn-danger btn-sm" onclick="removeProduct(this, '${selectedOption.value}')">Remove</button></td>
    `;

    tableBody.appendChild(row);
    productSelect.selectedIndex = 0;

    updateGrandTotal();
    checkFormValidity(); // Kiểm tra lại form sau khi thêm sản phẩm
}

function updateTotal(input, index, availableQuantity) {
    const row = input.closest("tr");
    const quantity = row.querySelector(".quantity").value;
    const price = row.querySelector(".price").value;
    const totalCell = document.getElementById(`total-${index}`);

    // Kiểm tra số lượng không vượt quá số lượng có sẵn
    if (quantity > availableQuantity) {
        alert(`Cannot select more than the available quantity (${availableQuantity})`);
        row.querySelector(".quantity").value = availableQuantity; // Set lại số lượng là số lượng có sẵn
        return;
    }

    if (quantity < 1) {
        row.querySelector(".quantity").value = 1; // Không cho phép số âm hoặc 0
    }

    if (price < 0) {
        row.querySelector(".price").value = 0; // Không cho phép giá âm
    }

    const totalPrice = (quantity * price).toFixed(2);
    totalCell.textContent = `$${totalPrice}`;

    updateGrandTotal();
}

function updateGrandTotal() {
    let total = 0;
    document.querySelectorAll(".total-price").forEach(cell => {
        total += parseFloat(cell.textContent.replace("$", "")) || 0;
    });

    document.getElementById("totalPriceOut").value = `$${total.toFixed(2)}`;
}

function removeProduct(button, productId) {
    selectedProducts = selectedProducts.filter(id => id !== productId);
    button.closest("tr").remove();

    updateGrandTotal();
    checkFormValidity(); // Kiểm tra lại form sau khi xóa sản phẩm
}
