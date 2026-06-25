using AtmoSync.Shared;
using AtmoSync.Shared.Models;
using AtmoSync.Shared.Models.DtoModels;
using System.Net.Http.Json;

namespace AtmoSync.Web.Services
{
    public class DhtSensorApiService
    {
        private readonly HttpClient _httpClient;

        public DhtSensorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET ALL
        public async Task<ResponseModel<List<DHTSensorDto>>?> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<List<DHTSensorDto>>>("DHTSensor");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // GET LATEST
        public async Task<ResponseModel<DHTSensorDto>?> GetLatestAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<DHTSensorDto>>("DHTSensor/latest");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // GET LAST N
        public async Task<ResponseModel<List<DHTSensorDto>>?> GetLatestReadingsAsync(int count)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResponseModel<List<DHTSensorDto>>>($"DHTSensor/latest/{count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // CREATE
        public async Task<ResponseModel<long>?> CreateAsync(DHTSensorDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("DHTSensor", dto);

                return await response.Content.ReadFromJsonAsync<ResponseModel<long>>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Filter
        public async Task<ResponseModel<List<DHTSensorDto>>?> GetByDateRangeAsync( DateTime fromDate,DateTime toDate)
        {
            try
            {
                var response = $"DHTSensor/range??fromDate={fromDate:yyyy-MM-dd} & toDate={toDate:yyyy-MM-dd}";

                return await _httpClient.GetFromJsonAsync<ResponseModel<List<DHTSensorDto>>>(response);
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
                var response = await _httpClient.DeleteAsync($"DHTSensor/{id}");

                return await response.Content.ReadFromJsonAsync<ResponseModel<int>>();
            }
            catch (Exception )
            {
                return null;
            }
        }
    }
}