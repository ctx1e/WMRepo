using WMAPI.Models;
using WMAPI.Repository.Interfaces;

namespace WMAPI.Repository.Implementations
{
    public class WarehouseOutRepository : IWarehouseOutRepository
    {
        private readonly WarehouseManagementContext _context;
        public WarehouseOutRepository(WarehouseManagementContext context)
        {
            _context = context;
        }
    }
}
