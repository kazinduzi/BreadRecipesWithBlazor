using BreadRecipesWithWasmBlazor.Client.ViewModels;

namespace BreadRecipesWithWasmBlazor.Client.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherForecast>> GetWeatherAsync();
    }
}