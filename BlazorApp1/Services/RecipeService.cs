using System.Net.Http.Json;

using BreadRecipesWithWasmBlazor.Client.ViewModels;

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
            var recipe = await _httpClient.GetFromJsonAsync<RecipeModel>($"api/recipe/{id}");
            return recipe;
        }

        public async Task<IEnumerable<RecipeModel>> GetRecipesAsync()
        {
            var recipes = await _httpClient.GetFromJsonAsync<IEnumerable<RecipeModel>>("api/recipe");
            return recipes;
        }

        public async Task<bool> CreateRecipeAsync(RecipeInputModel model)
        {
            var resp = await _httpClient.PostAsJsonAsync("api/recipe/create", model);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateRecipeAsync(int id, RecipeInputModel model)
        {
            var resp = await _httpClient.PutAsJsonAsync($"api/recipe/update/{id}", model);
            return resp.IsSuccessStatusCode;
        }
    }
}
