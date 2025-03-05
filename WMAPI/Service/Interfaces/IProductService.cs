using WMAPI.DTO;
using WMAPI.Models;

namespace WMAPI.Service.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<Product> products, string Msg)> GetAllProducts();
        Task<(Product? product, string Msg)> GetProductById(int id);
        Task<(bool IsSuccess, string Msg)> AddProduct(ProductDTO product);
        Task<(bool IsSuccess, string Msg)> UpdateProduct(ProductDTO product);
        Task<(bool IsSuccess, string Msg)> DeleteProduct(int id);
    }
}
