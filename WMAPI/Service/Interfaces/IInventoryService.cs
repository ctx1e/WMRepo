using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetAllInventories();
        Task<bool> DeleteProductInInventory(int productId);
    }
}
