using BlazorAppWASM.Shared.BusinessDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAppWASM.Server.Data
{
	public class RecipeRepository : IRecipeRepository
	{
		private readonly BreadRecipesDbContext _context;

		public RecipeRepository(BreadRecipesDbContext context)
		{
			_context = context;
		}

		public void AddRecipe(Recipe recipe)
		{
			_context.Recipes.Add(recipe);
			_context.SaveChanges();
		}

		public void DeleteRecipe(Recipe recipe)
		{
			var exist = _context.Recipes.Any(a => a.ObjectId == recipe.ObjectId);

			if (exist)
			{
				_context.Recipes.Remove(recipe);
				_context.SaveChanges();
			}
		}

		public Recipe GetRecipeById(int id)
		{
			return _context.Recipes.AsNoTracking().FirstOrDefault(r => r.ObjectId == id);
		}

		public IEnumerable<Recipe> GetRecipes()
		{
			return _context.Recipes.AsNoTracking().Include(i => i.Ingredients).ToList();
		}

		public void UpdateRecipe(int id, Recipe recipe)
		{
			var recipeDb = _context.Recipes.FirstOrDefault(r => r.ObjectId == id);
			if (recipeDb != null)
			{
				recipeDb = recipe;
				_context.SaveChanges();
			}
		}
	}
}
