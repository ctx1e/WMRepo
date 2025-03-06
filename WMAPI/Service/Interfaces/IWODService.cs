using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWODService
    {
        Task<(IEnumerable<WarehouseOutDetail>, string Msg)> GetAllWODs(int outId);

    }
}
