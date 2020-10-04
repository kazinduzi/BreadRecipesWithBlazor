using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BreadRecipesWithWasmBlazor.Client.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;

namespace BreadRecipesWithWasmBlazor.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");


			builder.Services.AddScoped(sp => 
			{
				var apiUrl = new Uri(builder.Configuration["API:BaseUrl"]);
				return new HttpClient { BaseAddress = apiUrl };			
			});

			//Register Authorization
			builder.Services.AddAuthorizationCore();
			builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();

			await builder.Build().RunAsync();
		}
	}
}
