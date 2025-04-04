﻿using WMAPI.DTO;
using WMAPI.Models;
using WMAPI.Repository.Interfaces;
using WMAPI.Service.Interfaces;

namespace WMAPI.Service.Implementations
{
    public class WIDService : IWIDService
    {
        private readonly IWIDRepository _widRepository;

        public WIDService(IWIDRepository widRepository)
        {
            _widRepository = widRepository;
        }
        public async Task<List<WIDListByInId>> GetAllWIDs(int inId)
        {
            var getWIDs = await _widRepository.GetAllWIDByInId(inId);

            if (!getWIDs.Any())
            {
                return new List<WIDListByInId>();
            }
            return getWIDs;
        }
    }
}
