using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddWO(WarehouseOut warehouseOut)
        {
            try
            {
                await _context.AddAsync(warehouseOut);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

 
        public async Task<bool> DeleteWO(WarehouseOut warehouseOut)
        {
            try
            {
                _context.Remove(warehouseOut);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<WarehouseOut>> GetAllWO()
        => await _context.WarehouseOuts.ToListAsync();

        public async Task<WarehouseOut?> GetWOById(int outId)
        => await _context.WarehouseOuts.FindAsync(outId);
    }
}
