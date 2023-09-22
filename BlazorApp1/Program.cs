using BreadRecipesWithWasmBlazor.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var serverBaseUri = "https://localhost:44301"; //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serverBaseUri) });

await builder.Build().RunAsync();
