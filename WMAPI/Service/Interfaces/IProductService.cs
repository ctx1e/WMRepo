using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product?> GetProductById(int productId);
        Task<bool> AddProduct(ProductDTO product);
        Task<bool> UpdateProduct(ProductDTO product);
        Task<bool> DeleteProduct(int proId);
    }
}
