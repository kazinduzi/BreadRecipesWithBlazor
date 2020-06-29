using BlazorAppWASM.Shared.BusinessDomain;
using System.Collections.Generic;
using System.Text;

namespace BlazorAppWASM.Server.Data
{
	public interface IRecipeRepository
	{
		bool saveChanges();

		IEnumerable<Recipe> GetRecipes();

		Recipe GetRecipeById(int id);

		void DeleteRecipe(Recipe recipe);

		void AddRecipe(Recipe recipe);

		void UpdateRecipe(int id, Recipe recipe);
	}
}
