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
        private readonly IWIDRepository _widRepository;
        private readonly IWODRepository _wodRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly Cloudinary _cloudinary;

        public ProductService(IProductRepository productRepository, IInventoryRepository inventoryRepository, Cloudinary cloudinary,
            IWIDRepository widRepository, IWODRepository wodRepository)
        {
            _productRepository = productRepository;
            _widRepository = widRepository;
            _wodRepository = wodRepository;
            _inventoryRepository = inventoryRepository;
            _cloudinary = cloudinary;
        }

        public async Task<bool> AddProduct(ProductDTO product)
        {
            if (!(await _productRepository.CheckNameProduct(product.ProductName, null)))
                return false;

            var getUrlImg = await UploadImageAsync(product.Image);
            if (getUrlImg == null) return false;

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Category = product.Category,
                Description = product.Description,
                Image = getUrlImg,
            };

            if (!(await _productRepository.AddProduct(newProduct)))
                return false;

            var newInventory = new Inventory
            {
                ProductId = newProduct.ProductId,
                QuantityInStock = 0,
                LastUpdated = DateTime.Now,

            };

            if (!(await _inventoryRepository.AddProductIntoInventory(newInventory)))
                return false;

            return true;
        }

        public async Task<bool> DeleteProduct(int proId)
        {
            var getProductById = await GetProductById(proId);
            if (getProductById == null) return false;

            //var getAllWIByProId = await _widRepository.GetAllWIByProductId(proId);
            //var getAllWOByProId = await _wodRepository.GetAllWOByProductId(proId);
            //foreach (var item in getAllWIByProId)
            //{
            //    item.ProductId = 0;
            //}
            //foreach (var item in getAllWOByProId)
            //{
            //    item.ProductId = 0;
            //}

            //if (!(await _widRepository.DeleteMultiWIDByInId(getAllWIByProId)))
            //    return false;

            //if (!(await _wodRepository.DeleteMultiWODByInId(getAllWOByProId)))
            //    return false;

            if (!(await _productRepository.DeleteProduct(getProductById)))
                return false;
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var getProducts = await _productRepository.GetAllProducts();

            if (!getProducts.Any())
            {
                return Enumerable.Empty<Product>();
            }
            return getProducts;
        }

        public async Task<Product?> GetProductById(int proId)
        {
            var getProductById = await _productRepository.GetProductById(proId);
            if (getProductById == null)
            {
                return null;
            }
            return getProductById;
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            var getProductById = await GetProductById(product.ProductId.Value);
            if (getProductById == null) return false;

            if (!(await _productRepository.CheckNameProduct(product.ProductName, product.ProductId.Value)))
                return false;

            // Check Img and get Url
            if (product.Image != null)
            {
                var getUrlImg = await UploadImageAsync(product.Image);
                getProductById.Image = getUrlImg;

            }


            getProductById.ProductName = product.ProductName;
            getProductById.Category = product.Category;
            getProductById.Description = product.Description;
            if (!(await _productRepository.UpdateProduct(getProductById)))
                return false;

            return true;
        }



        // Handle Image
        public async Task<string?> UploadImageAsync(IFormFile image)
        {
            long maxFileSize = 5 * 1024 * 1024;

            if (image == null || image.Length == 0)
            {
                return null;
            }

            if (image.Length > maxFileSize)
            {
                return null;
            }
            var id = await GenerateShortUniqueCode();
            try
            {
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        //File = new FileDescription(image.FileName, stream),
                        //Folder = "/WMProject/Img_products", 
                        //PublicId = $"product_image_{id}",

                        File = new FileDescription(image.FileName, stream),
                        UploadPreset = "wmproject_img_products",
                        UseFilename = true,     // Dùng tên file gốc làm PublicId
                        UniqueFilename = true
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams); // Use UploadAsync for file < 100MB
                    return uploadResult.SecureUrl.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GenerateShortUniqueCode()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            byte[] shortBytes = new byte[6];
            Array.Copy(bytes, 0, shortBytes, 0, 6); // Get 6 byte (48-bit)
            return Convert.ToBase64String(shortBytes).Substring(0, 8); // 8 characters
        }
    }
}
