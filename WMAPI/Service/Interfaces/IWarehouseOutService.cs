using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWarehouseOutService
    {
        Task<(IEnumerable<WarehouseOut> GetWOs, string Msg)> GetAllWOs();
        Task<(WarehouseOut? GetWOs, string Msg)> GetWOById(int OutId);
        Task<(bool IsSuccess, string Msg)> AddWO(GetWarehouseOutRequest warehouseOutRequest);
    }
}
