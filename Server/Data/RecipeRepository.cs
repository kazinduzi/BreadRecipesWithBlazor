using BlazorAppWASM.Shared.BusinessDomain;
using System;
using System.Collections.Generic;

namespace BlazorAppWASM.Server.Data
{
	public class RecipeRepository : IRecipeRepository
	{
		public void AddRecipe(Recipe recipe)
		{
			throw new NotImplementedException();
		}

		public void DeleteRecipe(Recipe recipe)
		{
			throw new NotImplementedException();
		}

		public Recipe GetRecipeById(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Recipe> GetRecipes()
		{
			throw new NotImplementedException();
		}

		public bool saveChanges()
		{
			throw new NotImplementedException();
		}

		public void UpdateRecipe(int id, Recipe recipe)
		{
			throw new NotImplementedException();
		}
	}
}
