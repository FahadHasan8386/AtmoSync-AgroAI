using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
using System.Net.Http.Json;

namespace AtmoSync.Web.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseModel<string>?> RegisterAsync(RegisterDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "Auth/register",
                    dto);

                return await response.Content
                    .ReadFromJsonAsync<ResponseModel<string>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ResponseModel<LoginResponseDto>?> LoginAsync(LoginDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "Auth/login",
                    dto);

                return await response.Content
                    .ReadFromJsonAsync<ResponseModel<LoginResponseDto>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
