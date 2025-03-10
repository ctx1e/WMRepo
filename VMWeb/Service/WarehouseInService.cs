using Newtonsoft.Json;
using System.Text;
using VMWeb.Models;
using VMWeb.Models.ViewModel;

namespace VMWeb.Service
{
    public class WarehouseInService
    {

        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7000/api/WarehouseIn";
        private readonly string API_URL_WID = "https://localhost:7000/api/WID";

        public WarehouseInService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WarehouseIn>> GetWarehouseInsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL}/getWarehouseIns");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<List<WarehouseIn>>(data));

                }
                else
                {
                    return (new List<WarehouseIn>());
                }
            }
            catch (HttpRequestException)
            {
                return (new List<WarehouseIn>());
            }
        }

        public async Task<WarehouseIn?> GetWarehouseInByInIdAsync(int InId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL}/getWIById/{InId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<WarehouseIn>(data));

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
        public async Task<List<WarehouseInDetail>> GetWIDByInIdAsync(int inId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{API_URL_WID}/getAllByInId/{inId}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return (JsonConvert.DeserializeObject<List<WarehouseInDetail>>(data));
                }
                else
                {
                    return (new List<WarehouseInDetail>());
                }
            }
            catch (HttpRequestException)
            {
                return (new List<WarehouseInDetail>());
            }
        }

        public async Task<bool> AddWarehouseInAsync(WarehouseInAddResponse warehouseIn)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(warehouseIn), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{API_URL}/addWarehouseIn", jsonContent);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteWarehouseInAsync(int inId)
        {
            try
            {

                var response = await _httpClient.DeleteAsync($"{API_URL}/deleteWI/{inId}");

   
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
