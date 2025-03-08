
const URl_API = "https://localhost:7000/api"
// Get API get all inventory
document.addEventListener("DOMContentLoaded", async () => {
    try {
        const response = await axios.get(`${URl_API}/Inventory`);

        // Check response
        if (response.status != 200) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // Get data
        console.log(response);
        const inventoryData = response.data.data;
        console.log(inventoryData);
        const tableBody = document.getElementById("inventory-body");
        tableBody.innerHTML = "";
        let count = 0;
        inventoryData.forEach(({ inventoryId, productName, quantityInStock, lastUpdated }) => {

            count += 1;
            const dateObj = new Date(lastUpdated);
            const formattedDate = `${dateObj.getDate().toString().padStart(2, '0')}/` +
                `${(dateObj.getMonth() + 1).toString().padStart(2, '0')}/` +
                `${dateObj.getFullYear().toString()}`;

            const row = document.createElement("tr");

            row.innerHTML = `
        <td>${count}</td>
        <td>${productName}</td>
        <td>${quantityInStock}</td>
        <td>${formattedDate}</td>
        `;
            tableBody.appendChild(row);
        });
    } catch (error) {
        console.log("Error when call API: ", error)
    }
});
