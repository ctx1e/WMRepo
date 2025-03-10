using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWODService
    {
        Task<IEnumerable<WODListByOutId>> GetAllWODs(int outId);


    }
}
