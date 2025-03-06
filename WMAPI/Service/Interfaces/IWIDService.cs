using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWIDService
    {
        Task<(IEnumerable<WarehouseInDetail>, string Msg)> GetAllWIDs(int inId);
        

    }
}
