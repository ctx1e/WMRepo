using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> AddMultiWODByInId(List<WarehouseOutDetail> wods)
        {
            try
            {
                await _context.AddRangeAsync(wods);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMultiWODByInId(List<WarehouseOutDetail> wods)
        {
            try
            {
                //_context.RemoveRange(wods);
                _context.UpdateRange(wods);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<WarehouseOutDetail>> GetAllWOByInId(int outId)
        => await _context.WarehouseOutDetails.Where(x => x.OutId == outId).ToListAsync();

        public async Task<List<WarehouseOutDetail>> GetAllWOByProductId(int proId)
        => await _context.WarehouseOutDetails.Where(x => x.ProductId == proId).ToListAsync();
    }
}
