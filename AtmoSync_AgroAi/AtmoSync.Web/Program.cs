using AtmoSync.Web;
using AtmoSync.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7276/api/") });

builder.Services.AddScoped<DhtSensorApiService>();
builder.Services.AddScoped<MQ136SensorApiService>();
builder.Services.AddScoped<MQ7SensorApiService>();

await builder.Build().RunAsync();
