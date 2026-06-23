using AtmoSync.Shared;
using System.Net.Http.Json;

namespace AtmoSync.Web.Services;

public class DhtSensorApiService
{
    private readonly HttpClient _httpClient;

    private const string BaseRoute = "api/DHTSensor";

    public DhtSensorApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResponseModel?> GetAllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel>(BaseRoute);
        }
        catch
        {
            return null;
        }
    }

    public async Task<ResponseModel?> GetLatestAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel>($"{BaseRoute}/latest");
        }
        catch
        {
            return null;
        }
    }

    public async Task<ResponseModel?> GetLatestReadingsAsync(int count)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel>($"{BaseRoute}/latest/{count}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<ResponseModel?> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel>(
                $"{BaseRoute}/range?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateAsync(double temperature, double humidity)
    {
        var request = new
        {
            Temperature = temperature,
            Humidity = humidity
        };

        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseRoute, request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseRoute}/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}