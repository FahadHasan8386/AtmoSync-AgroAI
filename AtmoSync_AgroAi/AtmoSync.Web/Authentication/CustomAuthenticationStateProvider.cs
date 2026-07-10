using AtmoSync.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AtmoSync.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TokenService _tokenService;

        public CustomAuthenticationStateProvider (TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenService.GetAccessTokenAsync();

            if(string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(accessToken);

            var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
