using Newtonsoft.Json;
using VMWeb.Models;

namespace VMWeb.Service
{
    public class WarehouseOutService
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7000/api/WarehouseOut";
        private readonly string API_URL_WOD = "https://localhost:7000/api/WOD";

        public WarehouseOutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WarehouseOut>> GetWarehouseOutsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL}/getWarehouseOuts");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<List<WarehouseOut>>(data));

                }
                else
                {
                    return (new List<WarehouseOut>());
                }
            }
            catch (HttpRequestException)
            {
                return (new List<WarehouseOut>());
            }
        }

        public async Task<WarehouseOut?> GetWarehouseOutByOutIdAsync(int outId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL}/getWOById/{outId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<WarehouseOut>(data));

                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
        public async Task<List<WarehouseOutDetail>> GetWODByOutIdAsync(int outId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL_WOD}/getAllByOutId/{outId}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<List<WarehouseOutDetail>>(data));
                }
                else
                {
                    return (new List<WarehouseOutDetail>());
                }
            }
            catch (HttpRequestException)
            {
                return (new List<WarehouseOutDetail>());
            }
        }
    }
}
