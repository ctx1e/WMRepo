using WMAPI.Models;

namespace WMAPI.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<Inventory?> GetProductInInventoryByProductId(int proId);
        Task<bool> AddProductIntoInventory(Inventory inventory);
        Task<bool> UpdateInventory(Inventory inventory);
        Task<bool> UpdateMultiInventory(List<Inventory> inventories);
        Task<bool> DeleteProductInInventory(Inventory inventory);
    }
}
