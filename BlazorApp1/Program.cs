using BreadRecipesWithWasmBlazor.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var serverBaseUri = builder.Configuration.GetSection("API").GetValue<Uri>("BaseUrl");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = serverBaseUri });

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}

await builder.Build().RunAsync();
