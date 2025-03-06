using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWarehouseInService
    {
        Task<(IEnumerable<WarehouseIn> GetWIs, string Msg)> GetAllWIs();
        Task<(WarehouseIn? GetWIs, string Msg)> GetWIById(int inId);
        Task<(bool IsSuccess, string Msg)> AddWI(GetWarehouseInRequest warehouseInRequest);
    }
}
