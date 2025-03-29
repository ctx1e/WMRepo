using Microsoft.EntityFrameworkCore;
using WMAPI.DTO;
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
        public async Task<bool> RemoveMultiWIDByWI(List<WIDListByInId> wids)
        {
            try
            {
                _context.RemoveRange(wids);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<WIDListByInId>> GetAllWIDByInId(int inId)
        => await _context.WarehouseInDetails.Where(x => x.InId == inId).Select(w => new WIDListByInId
        {
            InDetailId = w.InDetailId,
            InId = w.InId,
            ProductName = w.Product.ProductName,
            PriceIn = w.PriceIn,
            QuantityIn = w.QuantityIn,
            TotalPrice = w.TotalPrice,
        }).ToListAsync();

        public async Task<List<WarehouseInDetail>> GetAllWIByProductId(int proId)
       => await _context.WarehouseInDetails.Where(x => x.ProductId == proId).ToListAsync();

        public Task<WarehouseInDetail> GetWIDByProductId(int proId)
        {
            throw new NotImplementedException();
        }


       
    }
}
