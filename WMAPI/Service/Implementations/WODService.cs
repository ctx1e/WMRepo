using WMAPI.DTO;
using WMAPI.Models;
using WMAPI.Repository.Interfaces;
using WMAPI.Service.Interfaces;

namespace WMAPI.Service.Implementations
{
    public class WODService : IWODService
    {
        private readonly IWODRepository _wodRepository;

        public WODService(IWODRepository wodRepository)
        {
            _wodRepository = wodRepository;
        }
        public async Task<IEnumerable<WODListByOutId>> GetAllWODs(int outId)
        {
            var getWODs = await _wodRepository.GetAllWOByOutId(outId);

            if (!getWODs.Any())
            {
                return Enumerable.Empty<WODListByOutId>();
            }
            return getWODs;
        }
    }
}
