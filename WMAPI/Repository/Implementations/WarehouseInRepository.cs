using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddWI(WarehouseIn warehouseIn)
        {
            try
            {
                await _context.AddAsync(warehouseIn);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { 
                return false;
            }
        }

        public async Task<bool> DeleteWI(WarehouseIn warehouseIn)
        {
            try
            {
                _context.Remove(warehouseIn);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public async Task<IEnumerable<WarehouseIn>> GetAllWI()
        => await _context.WarehouseIns.OrderByDescending(x => x.InDate).ToListAsync();

        public async Task<WarehouseIn?> GetWIById(int inId)
        => await _context.WarehouseIns.FindAsync(inId);
    }
}
