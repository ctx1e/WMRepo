using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Repository.Interfaces
{
    public interface IWIDRepository
    {

        Task<List<WIDListByInId>> GetAllWIDByInId(int inId);
        Task<List<WarehouseInDetail>> GetAllWIByProductId(int proId);
        Task<bool> AddMultiWIDByInId(List<WarehouseInDetail> wids);
        Task<bool> DeleteMultiWIDByInId(List<WarehouseInDetail> wids);
        Task<bool> RemoveMultiWIDByWI(List<WIDListByInId> wids);


    }
}
