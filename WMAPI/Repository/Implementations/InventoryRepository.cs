using Microsoft.EntityFrameworkCore;
using WMAPI.DTO;
using WMAPI.Models;
using WMAPI.Repository.Interfaces;

namespace WMAPI.Repository.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {

        private readonly WarehouseManagementContext _context;
        public InventoryRepository(WarehouseManagementContext context)
        {
            _context = context;
        }

        public async Task<Inventory> getQISbyProId(int proId)
        {
            var getInvenById = await _context.Inventories.FirstOrDefaultAsync(x => x.ProductId == proId);

            return getInvenById;
        }
        public async Task<bool> AddProductIntoInventory(Inventory inventory)
        {
            try
            {
                await _context.AddAsync(inventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductInInventory(Inventory inventory)
        {
            try
            {
                _context.Remove(inventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<InventoryDTO>> GetAllInventories()
        => await _context.Inventories
                .Select(i => new InventoryDTO
                {
                    InventoryId = i.InventoryId,
                    ProductName = i.Product.ProductName,
                    QuantityInStock = i.QuantityInStock,
                    LastUpdated = i.LastUpdated
                })
                .OrderByDescending(x => x.LastUpdated)
                .ToListAsync();


        public async Task<Inventory?> GetProductInInventoryByProductId(int proId)
        => await _context.Inventories.FirstOrDefaultAsync(x => x.ProductId == proId);

        public async Task<bool> UpdateInventory(Inventory inventory)
        {
            try
            {
                _context.Update(inventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMultiInventory(List<Inventory> inventories)
        {
            try
            {

                _context.UpdateRange(inventories);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
