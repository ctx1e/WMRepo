using WMAPI.Models;

namespace WMAPI.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventories();
        Task<bool> AddProductIntoInventory(Inventory inventory);
        Task<bool> UpdateInventory(Inventory inventory);
        Task<bool> DeleteProductInInventory(Inventory inventory);
    }
}
