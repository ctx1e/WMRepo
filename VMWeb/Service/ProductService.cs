using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using VMWeb.Models;
using VMWeb.Models.ViewModel;

namespace VMWeb.Service
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7000/api/Product";
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductViewModel>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL}/getListProducts");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<List<ProductViewModel>>(data));

                }
                else
                {
                    return (new List<ProductViewModel>());
                }
            }
            catch (HttpRequestException)
            {
                return (new List<ProductViewModel>());
            }
        }

        public async Task<ProductViewModel?> GetProductByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL}/getProductById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<ProductViewModel>(data));
                } else
                {
                    return null;
                }
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }


        public async Task<bool> AddProductAsync(Product product)
        {
            try
            {
                var content = new MultipartFormDataContent();

                // Thêm dữ liệu sản phẩm vào content (product)
                content.Add(new StringContent(product.ProductName), "ProductName");
                content.Add(new StringContent(product.Category), "Category");
                content.Add(new StringContent(product.Description), "Description");

                // Kiểm tra nếu có file thì thêm vào content
                if (product.Image != null && product.Image.Length > 0)
                {
                    // Kiểm tra loại ảnh
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(product.Image.FileName).ToLower();

                    if (allowedExtensions.Contains(fileExtension))
                    {
                        var fileContent = new StreamContent(product.Image.OpenReadStream());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue($"image/{fileExtension.Substring(1)}");
                        content.Add(fileContent, "Image", product.Image.FileName); 
                    }
                    else
                    {
                        return false; 
                    }
                }

                // Gửi yêu cầu POST đến API
                var response = await _httpClient.PostAsync(API_URL, content);

                // Kiểm tra xem API có trả về thành công không
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                // Chuyển đối tượng ProductViewModel thành JSON
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                // Gửi yêu cầu PUT đến API để cập nhật sản phẩm
                var response = await _httpClient.PutAsync($"{API_URL}/UpdateProduct", content);

                // Kiểm tra xem API có trả về thành công không
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                // Gửi yêu cầu DELETE đến API để xóa sản phẩm
                var response = await _httpClient.DeleteAsync($"{API_URL}/Product/Delete/{productId}");

                // Kiểm tra xem API có trả về thành công không
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
