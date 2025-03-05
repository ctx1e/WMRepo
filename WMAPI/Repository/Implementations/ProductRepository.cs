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

        public async Task<bool> CheckNameProduct(string productName)
        {
            try
            {
                var check = await _context.Products.FirstOrDefaultAsync(x => x.ProductName == productName);
                if (check == null) return false;
                return true;
            }
            catch (Exception) {
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
        => await _context.Products.ToListAsync();

        public async Task<Product?> GetProductById(int id)
        => await _context.Products.FindAsync(id);

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
