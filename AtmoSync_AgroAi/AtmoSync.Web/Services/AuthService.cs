using AtmoSync.Shared;
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

        public async Task<bool> LoginAsync(LoginDto user)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login",user);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var result =  await response.Content.ReadFromJsonAsync<ResponseModel<LoginResponseDto>>();

            if (result?.Data == null)
            {
                return false;
            }

            await _tokenService.SaveTokensAsync( result.Data.Token, result.Data.RefreshToken);

            _authProvider.NotifyUserAuthentication( result.Data.Token);

            return true;
        }

        public async Task<ResponseModel<string>?> RegisterAsync(RegisterDto dto)
        {
            var response =await _http.PostAsJsonAsync("api/Auth/register",dto);

            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
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
