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
        public async Task<IEnumerable<WarehouseOutDetail>> GetAllWODs(int outId)
        {
            var getWIDs = await _wodRepository.GetAllWOByInId(outId);

            if (!getWIDs.Any())
            {
                return Enumerable.Empty<WarehouseOutDetail>();
            }
            return getWIDs;
        }
    }
}
