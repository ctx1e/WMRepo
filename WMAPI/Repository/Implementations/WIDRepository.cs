using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddMultiWIDByInId(List<WarehouseInDetail> wids)
        {
            try
            {
                await _context.AddRangeAsync(wids);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMultiWIDByInId(List<WarehouseInDetail> wids)
        {
            try
            {
                //_context.RemoveRange(wids);
                _context.UpdateRange(wids);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<WarehouseInDetail>> GetAllWIByInId(int inId)
        => await _context.WarehouseInDetails.Where(x => x.InId == inId).ToListAsync();

        public async Task<List<WarehouseInDetail>> GetAllWIByProductId(int proId)
       => await _context.WarehouseInDetails.Where(x => x.ProductId == proId).ToListAsync();
    }
}
