using WMAPI.Models;
using WMAPI.Repository.Interfaces;

namespace WMAPI.Repository.Implementations
{
    public class WarehouseInRepository : IWarehouseInRepository
    {
        private readonly WarehouseManagementContext _context;
        public WarehouseInRepository(WarehouseManagementContext context)
        {
            _context = context;
        }
    }
}
