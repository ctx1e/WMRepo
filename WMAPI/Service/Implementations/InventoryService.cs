using WMAPI.Models;
using WMAPI.Repository.Interfaces;
using WMAPI.Service.Interfaces;

namespace WMAPI.Service.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<(bool IsSuccess, string Msg)> DeleteProductInInventory(int id)
        {
            var getInventory = await _inventoryRepository.GetProductInInventoryByProductId(id);
            if (getInventory == null) return (false, "Not found Product" + id);

            if (!(await _inventoryRepository.DeleteProductInInventory(getInventory)))
                return (false, "Delete Product In Inventory failed!");

            return (true, "");
        }


        public async Task<(IEnumerable<Inventory> inventories, string Msg)> GetAllInventories()
        {
            var getInventories = await _inventoryRepository.GetAllInventories();

            if (!getInventories.Any())
            {
                return (Enumerable.Empty<Inventory>(), "Inventories is empty");
            }
            return (getInventories, "get Inventories successfully");
        }
    }
}
