using Newtonsoft.Json;
using System.Text.Json.Serialization;
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
                var response = await _httpClient.GetAsync(API_URL + "/getListProducts");
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
    }
}
