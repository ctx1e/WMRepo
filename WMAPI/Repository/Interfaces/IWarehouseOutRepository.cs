using WMAPI.Models;

namespace WMAPI.Repository.Interfaces
{
    public interface IWarehouseOutRepository
    {
        Task<IEnumerable<WarehouseOut>> GetAllWO();
        Task<WarehouseOut?> GetWOById(int outId);
        Task<bool> AddWO(WarehouseOut warehouseOut);
        Task<bool> DeleteWO(WarehouseOut warehouseOut);
    }
}
