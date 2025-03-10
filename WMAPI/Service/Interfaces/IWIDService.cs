using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IWIDService
    {
        Task<List<WIDListByInId>> GetAllWIDs(int inId);
        

    }
}
