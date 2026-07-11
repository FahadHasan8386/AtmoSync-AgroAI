using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
using System.Net.Http.Json;

namespace AtmoSync.Web.Services
{
    public class MQ136SensorApiService
    {
        private readonly HttpClient _httpClient;

        public MQ136SensorApiService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //Get All
        public async Task<ResponseModel<List<MQ136SensorDto>>?> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<List<MQ136SensorDto>>>("api/MQ136Sensor");
            }
            catch(Exception )
            {
                return null;
            }
        }
        // Get LATEST
        public async Task<ResponseModel<MQ136SensorDto>?> GetLatestAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<MQ136SensorDto>>("api/MQ136Sensor/latest");
            }
            catch (Exception )
            {
                return null;
            }
        }
        // GET LAST N
        public async Task<ResponseModel<List<MQ136SensorDto>>?> GetLatestReadingsAsync(int count)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<List<MQ136SensorDto>>>($"api/MQ136Sensor/latest/{count}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        // CREATE
        public async Task<ResponseModel<long>?> CreateAsync(MQ136SensorDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MQ136Sensor", dto);

                return await response.Content.ReadFromJsonAsync<ResponseModel<long>>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResponseModel<List<MQ136SensorDto>>?> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var url = $"api/MQ136Sensor/range?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}";

                return await _httpClient.GetFromJsonAsync<ResponseModel<List<MQ136SensorDto>>>(url);
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
                var response = await _httpClient.DeleteAsync($"api/MQ136Sensor/{id}");

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
                var response = await _httpClient.PutAsync($"api/MQ136Sensor/{id}/status?inActive={inActive}", null);

                return await response.Content.ReadFromJsonAsync<ResponseModel<int>>();
            }
            catch
            {
                return null;
            }
        }
    }
}
