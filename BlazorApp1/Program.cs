using BreadRecipesWithWasmBlazor.Client;
using BreadRecipesWithWasmBlazor.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var serverBaseUri = builder.Configuration.GetSection("API").GetValue<Uri>("BaseUrl");

builder.Services.AddHttpClient("ServerApi")
    .ConfigureHttpClient(client => client.BaseAddress = serverBaseUri);

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ILoginService, LoginService>();

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}

await builder.Build().RunAsync();
