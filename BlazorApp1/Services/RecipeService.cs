using BreadRecipesWithWasmBlazor.Client.ViewModels;
using System.Net.Http.Json;

namespace BreadRecipesWithWasmBlazor.Client.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ServerApi");
        }

		public async Task<RecipeModel> GetRecipeByIdAsync(int id)
		{
            var recipe = await _httpClient.GetFromJsonAsync<RecipeModel>($"recipe/{id}");
            return recipe;
		}

		public async Task<IEnumerable<RecipeModel>> GetRecipesAsync()
        {
            var recipes = await _httpClient.GetFromJsonAsync<IEnumerable<RecipeModel>>("recipe");
            return recipes;
        }
    }
}
