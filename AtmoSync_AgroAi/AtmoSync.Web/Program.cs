using AtmoSync.Web;
using AtmoSync.Web.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7276/api/") });

// LocalStorage register 
builder.Services.AddBlazoredLocalStorage();

// Authorization 
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<DhtSensorApiService>();
builder.Services.AddScoped<MQ136SensorApiService>();
builder.Services.AddScoped<MQ7SensorApiService>();
builder.Services.AddScoped<CustomAuthStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<AuthApiService>();

await builder.Build().RunAsync();