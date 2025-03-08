using Newtonsoft.Json;
using VMWeb.Models;
using VMWeb.Models.ViewModel;

namespace VMWeb.Service
{
    public class InventoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7000/api/Inventory/inventories";

        public InventoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Inventory>> GetInventoryDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(API_URL);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<List<Inventory>>(jsonString));
                }
                else
                {
                    return (new List<Inventory>());
                }
            }
            catch (HttpRequestException)
            {
                return (new List<Inventory>());
            }

        }

    }
}



