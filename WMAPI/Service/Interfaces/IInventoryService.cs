using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IInventoryService
    {
        Task<(IEnumerable<Inventory> inventories, string Msg)> GetAllInventories();
        Task<(bool IsSuccess, string Msg)> DeleteProductInInventory(int productId);
    }
}
