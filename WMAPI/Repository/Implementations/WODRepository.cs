using WMAPI.Models;
using WMAPI.Repository.Interfaces;

namespace WMAPI.Repository.Implementations
{
    public class WODRepository : IWODRepository
    {
        private readonly WarehouseManagementContext _context;
        public WODRepository(WarehouseManagementContext context)
        {
            _context = context;
        }
    }
}
