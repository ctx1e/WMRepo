using WMAPI.Models;

namespace WMAPI.Repository.Interfaces
{
    public interface IWarehouseInRepository
    {
        Task<IEnumerable<WarehouseIn>> GetAllWI();
        Task<WarehouseIn?> GetWIById(int inId);
        Task<bool> AddWI(WarehouseIn warehouseIn);
        Task<bool> DeleteWI(WarehouseIn warehouseIn);
    }
}
