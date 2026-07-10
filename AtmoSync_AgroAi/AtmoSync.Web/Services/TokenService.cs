using Blazored.LocalStorage;

namespace AtmoSync.Web.Services
{
    public class TokenService
    {
        private readonly ILocalStorageService _localStorage;

        private const string AccessTokenKey = "accessToken";
        private const string RefreshTokenKey = "refreshToken";

        public TokenService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SaveTokensAsync(string accessToken , string refreshToken)
        {
            await _localStorage.SetItemAsync(AccessTokenKey, accessToken);

            await _localStorage.SetItemAsync(RefreshTokenKey, refreshToken);
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>(AccessTokenKey);
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetAccessTokenAsync();

            return !string.IsNullOrWhiteSpace(token);
        }

        public async Task RemoveTokensAsync()
        {
            await _localStorage
                .RemoveItemAsync(AccessTokenKey);


            await _localStorage
                .RemoveItemAsync(RefreshTokenKey);
        }
    }
}
