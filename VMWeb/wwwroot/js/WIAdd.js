let selectedProducts = [];

// Load danh sách sản phẩm từ API khi trang tải
document.addEventListener("DOMContentLoaded", function () {
    fetch('https://localhost:7000/api/Product/getListProducts')
        .then(response => response.json())
        .then(data => {
            const productSelect = document.getElementById("productSelect");
            data.forEach(product => {
                let option = document.createElement("option");
                option.value = product.productId;
                option.textContent = product.productName;
                productSelect.appendChild(option);
            });
        })
        .catch(error => console.error('Error loading products:', error));

    checkFormValidity(); // Kiểm tra ngay khi trang tải
});

function checkFormValidity() {
    const supplierSelect = document.getElementById("supplierSelect");
    const submitBtn = document.getElementById("submitBtn");
    const productTableBody = document.getElementById("productTableBody");

    let isSupplierSelected = supplierSelect.value.trim() !== "";
    let hasProducts = productTableBody.children.length > 0;

    submitBtn.disabled = !(isSupplierSelected && hasProducts);
}

// Khi chọn Supplier, kiểm tra lại điều kiện submit
document.getElementById("supplierSelect").addEventListener("change", checkFormValidity);

function addProduct() {
    const productSelect = document.getElementById("productSelect");
    const selectedOption = productSelect.options[productSelect.selectedIndex];

    if (!selectedOption.value) {
        alert("Please select a product!");
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
            <input type="hidden" name="WarehouseInDetailDTOs[${rowIndex}].ProductId" value="${selectedOption.value}" />
            <input type="text" class="form-control" name="WarehouseInDetailDTOs[${rowIndex}].ProductName" value="${selectedOption.text}" readonly>
        </td>
        <td><input type="number" class="form-control quantity" name="WarehouseInDetailDTOs[${rowIndex}].QuantityIn" min="1" value="1" oninput="updateTotal(this, ${rowIndex})"></td>
        <td><input type="number" class="form-control price" name="WarehouseInDetailDTOs[${rowIndex}].PriceIn" min="0.01" step="0.01" value="0" oninput="updateTotal(this, ${rowIndex})"></td>
        <td class="total-price" id="total-${rowIndex}">$0.00</td>
        <td><button type="button" class="btn btn-danger btn-sm" onclick="removeProduct(this, '${selectedOption.value}')">Remove</button></td>
    `;

    tableBody.appendChild(row);
    productSelect.selectedIndex = 0;

    updateGrandTotal();
    checkFormValidity(); // Kiểm tra lại form sau khi thêm sản phẩm
}

function removeProduct(button, productId) {
    selectedProducts = selectedProducts.filter(id => id !== productId);
    button.closest("tr").remove();

    updateGrandTotal();
    checkFormValidity(); // Kiểm tra lại form sau khi xóa sản phẩm
}

function updateTotal(input, index) {
    const row = input.closest("tr");
    const quantity = row.querySelector(".quantity").value;
    const price = row.querySelector(".price").value;
    const totalCell = document.getElementById(`total-${index}`);

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

    document.getElementById("totalPriceIn").value = `$${total.toFixed(2)}`;
}
