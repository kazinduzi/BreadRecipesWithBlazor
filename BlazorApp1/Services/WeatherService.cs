using BreadRecipesWithWasmBlazor.Client.ViewModels;
using System.Net.Http.Json;

namespace BreadRecipesWithWasmBlazor.Client.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerApi");
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherAsync()
        {
            var weatherForecast = await _httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("weather");
            return weatherForecast;
        }
    }
}
