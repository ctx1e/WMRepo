using Microsoft.EntityFrameworkCore;
using WMAPI.Models;
using WMAPI.Repository.Interfaces;

namespace WMAPI.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly WarehouseManagementContext _context;
        public ProductRepository(WarehouseManagementContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                await _context.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckNameProduct(string productName, int? proId = null)
        {
            try
            {

                // check for both add and update
                var check = await _context.Products
                    .FirstOrDefaultAsync(x => x.ProductName == productName && (proId == null || x.ProductId != proId));

                return check == null;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> DeleteProduct(Product product)
        {
            try
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        => await _context.Products.OrderByDescending(x => x.ProductId).ToListAsync();

        public async Task<Product?> GetProductById(int proId)
        => await _context.Products.FindAsync(proId);

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                _context.Update(product);
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
