using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
using System.Net.Http.Json;

namespace AtmoSync.Web.Services
{
    public class MQ7SensorApiService
    {

        private readonly HttpClient _httpClient;

        public MQ7SensorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //Get All
        public async Task<ResponseModel<List<MQ7SensorDto>>?> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<List<MQ7SensorDto>>>("MQ7Sensor");
            }
            catch (Exception)
            {
                return null;
            }
        }
        // Get LATEST
        public async Task<ResponseModel<MQ7SensorDto>?> GetLatestAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<MQ7SensorDto>>("MQ7Sensor/latest");
            }
            catch (Exception )
            {
                return null;
            }
        }
        // GET LAST N
        public async Task<ResponseModel<List<MQ7SensorDto>>?> GetLatestReadingsAsync(int count)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<List<MQ7SensorDto>>>($"MQ7Sensor/latest/{count}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        // CREATE
        public async Task<ResponseModel<long>?> CreateAsync(MQ7SensorDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("MQ7Sensor", dto);

                return await response.Content.ReadFromJsonAsync<ResponseModel<long>>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<MQ7SensorDto>>?> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var url = $"MQ7Sensor/range?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}";

                return await _httpClient.GetFromJsonAsync<ResponseModel<List<MQ7SensorDto>>>(url);
            }
            catch (Exception )
            {
                return null;
            }
        }

        // DELETE
        public async Task<ResponseModel<int>?> DeleteAsync(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"MQ7Sensor/{id}");

                return await response.Content.ReadFromJsonAsync<ResponseModel<int>>();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ResponseModel<int>?> UpdateStatusAsync(long id, bool inActive)
        {
            try
            {
                var response = await _httpClient.PutAsync($"MQ7Sensor/{id}/status?inActive={inActive}", null);

                return await response.Content.ReadFromJsonAsync<ResponseModel<int>>();
            }
            catch
            {
                return null;
            }
        }


    }
}
