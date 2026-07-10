using AtmoSync.Shared.Models.DtoModels;
using AtmoSync.Web.Authentication;
using System.Net.Http.Json;

namespace AtmoSync.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly TokenService _tokenService;
        private readonly CustomAuthenticationStateProvider _authProvider;

        public AuthService(HttpClient http, TokenService tokenService, CustomAuthenticationStateProvider authProvider)
        {
            _http = http;
            _tokenService = tokenService;
            _authProvider = authProvider;
        }

        public async Task<bool> Login(LoginDto user)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login", user);

            if(!response.IsSuccessStatusCode)
            {
                return false;
            }
            var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();

            if(result == null)
            {
                return false;
            }

            await _tokenService.SaveTokensAsync(result.AccessToken, result.RefreshToken);

            _authProvider.NotifyUserAuthentication(result.AccessToken);

            return true;
        }

        public async Task<bool> Logout()
        {
            try
            {
                var response = await _http.PostAsync("api/Auth/logout", null);

                await _tokenService.RemoveTokensAsync();

                _authProvider.NotifyUserLogout();

                return response.IsSuccessStatusCode;
            }
            catch
            {
                await _tokenService.RemoveTokensAsync();

                _authProvider.NotifyUserLogout();

                return false;
            }
        }
    }
}
