using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WMAPI.DTO;
using WMAPI.Models;
using WMAPI.Repository.Interfaces;
using WMAPI.Service.Interfaces;

namespace WMAPI.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly Cloudinary _cloudinary;

        public ProductService(IProductRepository productRepository, IInventoryRepository inventoryRepository,Cloudinary cloudinary)
        {
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _cloudinary = cloudinary;
        }

        public async Task<(bool IsSuccess, string Msg)> AddProduct(ProductDTO product)
        {
            if (!(await _productRepository.CheckNameProduct(product.ProductName)))
                return (false, "Product Name is already existed!");

            var (msg, getUrlImg) = await UploadImageAsync(product.Image);
            if (getUrlImg == null) return (false, msg);

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Category = product.Category,
                Description = product.Description,
                Image = getUrlImg,
            };

             if (!(await _productRepository.AddProduct(newProduct)))
                return (false, "Add product failed!");

            var newInventory = new Inventory
            {
                ProductId = newProduct.ProductId,
                QuantityInStock = 0,
                LastUpdated = DateTime.Now,
                
            };

            if (!(await _inventoryRepository.AddProductIntoInventory(newInventory)))
                return (false, "Add Product Into Inventory failed");

            return (true, "Add product successfully");
        }

        public Task<(bool IsSuccess, string Msg)> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<Product> products, string Msg)> GetAllProducts()
        {
            var getProducts = await _productRepository.GetAllProducts();

            if (!getProducts.Any())
            {
                return (Enumerable.Empty<Product>(), "Products is empty");
            }
            return (getProducts, "get Products successfully");
        }

        public async Task<(Product? product, string Msg)> GetProductById(int id)
        {
            var getProductById = await _productRepository.GetProductById(id);
            if (getProductById == null)
            {
                return (null, "Not found product!");
            }
            return (getProductById, "Get Product By Id Successfully");
        }

        public async Task<(bool IsSuccess, string Msg)> UpdateProduct(ProductDTO product)
        {
            var (getProductById, msg) = await GetProductById(product.ProductId);
            if (getProductById == null) return (false, msg);

            if (!(await _productRepository.CheckNameProduct(product.ProductName)))
                return (false, "Product Name is already existed!");

            getProductById.ProductName = product.ProductName;
            getProductById.Category = product.Category;
            getProductById.Description = product.Description;
            //getProductById.Image = product.Image;
            //if (!(await _productRepository.UpdateProduct(product)))
            //    return (false, "Update product failed!");

            return (true, "Update product successfully");
        }



        // Handle Image
        public async Task<(string Message, string? Url)> UploadImageAsync(IFormFile image)
        {
            long maxFileSize = 5 * 1024 * 1024;

            if (image == null || image.Length == 0)
            {
                return ("No images selected ", null);
            }   

            if (image.Length > maxFileSize)
            {
                return ($"The image is too large. The maximum size is {maxFileSize / (1024 * 1024)} MB.", null);
            }

            try
            {
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, stream),
                        Folder = "WMProject/Img_products", 
                        PublicId = $"product_image",
                        UniqueFilename = true
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams); // Use UploadAsync for file < 100MB
                    return ("", uploadResult.SecureUrl.ToString());
                }
            }
            catch (Exception ex)
            {
                return ($"Error when download image: {ex.Message}", null);
            }
        }
    }
}
