using BreadRecipesWithWasmBlazor.Client.ViewModels;

namespace BreadRecipesWithWasmBlazor.Client.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeModel>> GetRecipesAsync();

        Task<RecipeModel> GetRecipeByIdAsync(int id);

        Task<bool> CreateRecipeAsync(RecipeInputModel model);

        Task<bool> UpdateRecipeAsync(int id, RecipeInputModel model);
    }
}