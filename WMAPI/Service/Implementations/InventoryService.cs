using WMAPI.DTO;
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
        public async Task<bool> DeleteProductInInventory(int proId)
        {
            var getInventory = await _inventoryRepository.GetProductInInventoryByProductId(proId);
            if (getInventory == null) return false;

            if (!(await _inventoryRepository.DeleteProductInInventory(getInventory)))
                return false;

            return true;
        }


        public async Task<IEnumerable<InventoryDTO>> GetAllInventories()
        {
            var getInventories = await _inventoryRepository.GetAllInventories();

            if (!getInventories.Any())
            {
                return Enumerable.Empty<InventoryDTO>();
            }
            return getInventories;
        }

        public async Task<Inventory> getQISbyProId(int proId)
        => await _inventoryRepository.getQISbyProId(proId);
    }
}
