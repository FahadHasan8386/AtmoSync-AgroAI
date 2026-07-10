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

// Authorization
builder.Services.AddAuthorizationCore();


// Authentication State Provider
builder.Services.AddScoped<CustomAuthStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<CustomAuthStateProvider>());

// Token Service
builder.Services.AddScoped<TokenService>();

// JWT Handler
builder.Services.AddScoped<ApiAuthenticationHeaderHandler>();

// HttpClient with JWT token attach
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

builder.Services.AddScoped<DhtSensorApiService>();

builder.Services.AddScoped<MQ136SensorApiService>();

builder.Services.AddScoped<MQ7SensorApiService>();

// Authentication Service
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();