using WMAPI.Models;

namespace WMAPI.Repository.Interfaces
{
    public interface IWODRepository
    {
        Task<IEnumerable<WarehouseOutDetail>> GetAllWOByInId(int outId);
        Task<List<WarehouseOutDetail>> GetAllWOByProductId(int proId);

        Task<bool> AddMultiWODByInId(List<WarehouseOutDetail> wods);
        Task<bool> DeleteMultiWODByInId(List<WarehouseOutDetail> wods);
    }
}
