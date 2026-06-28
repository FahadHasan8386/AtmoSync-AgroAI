using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AtmoSync.Web.Services
{
    //public class CustomAuthStateProvider : AuthenticationStateProvider
    //{
    //    private readonly ILocalStorageService _localStorage;

    //    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    //    public CustomAuthStateProvider(ILocalStorageService localStorage)
    //    {
    //        _localStorage = localStorage;
    //    }


    //    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //    {
    //        string? Token = null;
    //    }
    //}
}
