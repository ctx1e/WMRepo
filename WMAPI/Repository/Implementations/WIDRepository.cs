using WMAPI.Models;
using WMAPI.Repository.Interfaces;

namespace WMAPI.Repository.Implementations
{
    public class WIDRepository : IWIDRepository
    {
        private readonly WarehouseManagementContext _context;
        public WIDRepository(WarehouseManagementContext context)
        {
            _context = context;
        }
    }
}
