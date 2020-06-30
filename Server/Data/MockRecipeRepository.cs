using BlazorAppWASM.Shared.BusinessDomain;
using BlazorAppWASM.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAppWASM.Server.Data
{
	public class MockRecipeRepository : IRecipeRepository
	{
		private readonly IEnumerable<Recipe> _recipes;

		public MockRecipeRepository()
		{
			_recipes = new List<Recipe>
			{
				new Recipe {
					ObjectId = 1,
					Name = "Pain francais",
					Duration = 120,
					HealthyLabel = HealthyLabel.Healthy,
					CreatedAt = DateTime.UtcNow,
					Ingredients = new List<Ingredient>
					{
						new Ingredient
						{
							Name = "Farine non-blanchie Tradition",
							Amount = "1 Kg"
						}
					}
				},

				new Recipe {
					ObjectId = 2,
					Name = "Croissant",
					Duration = 120,
					HealthyLabel = HealthyLabel.Unhealthy,
					CreatedAt = DateTime.UtcNow,
					Ingredients = new List<Ingredient>
					{
						new Ingredient
						{
							Name = "Farine type 1",
							Amount = "1 Kg"
						}
					}
				}
			};
		}

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
			return _recipes.FirstOrDefault(i => i.ObjectId == id);
		}

		public IEnumerable<Recipe> GetRecipes()
		{
			return _recipes;
		}

		public void UpdateRecipe(int id, Recipe recipe)
		{
			throw new NotImplementedException();
		}
	}
}
