using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AtmoSync.Web.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthStateProvider _authStateProvider;

        public AuthApiService(HttpClient httpClient,ILocalStorageService localStorage,AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;

            _authStateProvider = (CustomAuthStateProvider)authStateProvider;
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

                var result = await response.Content
                    .ReadFromJsonAsync<ResponseModel<LoginResponseDto>>();

                if (result is not null && result.Code == 200 && result.Data is not null && !string.IsNullOrWhiteSpace(result.Data.Token))
                {
                    var token = result.Data.Token;

                    await _localStorage.SetItemAsStringAsync("authToken", token);

                    // CustomAuthStateProvider knows user is authenticated
                    _authStateProvider.MarkUserAsAuthenticated(token);

                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _authStateProvider.MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}