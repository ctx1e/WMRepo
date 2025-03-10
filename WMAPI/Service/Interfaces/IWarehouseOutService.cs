using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWarehouseOutService
    {
        Task<IEnumerable<WarehouseOut>> GetAllWOs();
        Task<WarehouseOut?> GetWOById(int OutId);
        Task<bool> AddWO(GetWarehouseOutRequest warehouseOutRequest);

        Task<bool> DeleteWO(int outId);

    }
}
