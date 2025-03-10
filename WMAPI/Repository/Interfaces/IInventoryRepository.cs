using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryDTO>> GetAllInventories();
        Task<Inventory> getQISbyProId(int proId);
        Task<Inventory?> GetProductInInventoryByProductId(int proId);
        Task<bool> AddProductIntoInventory(Inventory inventory);
        Task<bool> UpdateInventory(Inventory inventory);
        Task<bool> UpdateMultiInventory(List<Inventory> inventories);
        Task<bool> DeleteProductInInventory(Inventory inventory);
    }
}
