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
        public async Task<List<DHTSensorDto>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DHTSensorDto>>("DHTSensor");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // GET LATEST
        public async Task<DHTSensorDto> GetLatestAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DHTSensorDto>("DHTSensor/latest");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // GET LAST N
        public async Task<List<DHTSensorDto>> GetLatestReadingsAsync(int count)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DHTSensorDto>>($"DHTSensor/latest/{count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // CREATE
        public async Task<bool> CreateAsync(DHTSensorDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("DHTSensor", dto);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // DELETE
        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"DHTSensor/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}