using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWarehouseInService
    {
        Task<IEnumerable<WarehouseIn>> GetAllWIs();
        Task<WarehouseIn?> GetWIById(int inId);
        Task<bool> AddWI(GetWarehouseInRequest warehouseInRequest);
        Task<bool> DeleteWI(int inId);

    }
}
