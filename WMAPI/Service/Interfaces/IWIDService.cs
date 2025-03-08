using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWIDService
    {
        Task<IEnumerable<WarehouseInDetail>> GetAllWIDs(int inId);
        

    }
}
