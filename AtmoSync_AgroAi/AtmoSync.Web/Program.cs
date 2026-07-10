using AtmoSync.Web;
using AtmoSync.Web.Authentication;
using AtmoSync.Web.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Local Storage
builder.Services.AddBlazoredLocalStorage();


// Authentication
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(
    provider =>
    provider.GetRequiredService<CustomAuthenticationStateProvider>());


// Token
builder.Services.AddScoped<TokenService>();


// JWT Handler
builder.Services.AddScoped<ApiAuthenticationHeaderHandler>();


// HttpClient
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<ApiAuthenticationHeaderHandler>();

    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7276/")
    };
});


// API Services
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<DhtSensorApiService>();
builder.Services.AddScoped<MQ136SensorApiService>();
builder.Services.AddScoped<MQ7SensorApiService>();


await builder.Build().RunAsync();