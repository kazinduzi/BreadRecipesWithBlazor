using AccountOwnerServer.Data;
using AccountOwnerServer.Dtos;
using AccountOwnerServer.Models;
using AccountOwnerServer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOwnerServer.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public RecipeController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<RecipeViewModel>>> Get()
		{
			var recipes = await _context.Recipes.AsNoTracking()
				.Include(i => i.RecipeIngredients)
				.Select(s => new RecipeViewModel
				{
					Id = s.Id,
					Name = s.Name,
					CreatedOn = s.CreatedOn,
					UpdatedOn = s.UpdatedOn,
					DurationInMinutes = s.DurationInMinutes,
					Ingredients = s.RecipeIngredients.Select(ri => 
						new IngredientViewModel { 
							Id = ri.IngredientId,
							Name = ri.Ingredient.Name,
							Quantity = ri.Ingredient.Quantity,
							UnityOfMeasure = ri.Ingredient.UnityOfMeasure
						})
				}).ToListAsync();

			return Ok(recipes);
		}

		[HttpPost]
		[Route("create")]
		public async Task<ActionResult<bool>> AddRecipe([FromBody] RecipeInputModel model)
		{
			if (ModelState.IsValid)
			{
				var recipe = new Recipe
				{
					Name = model.Name,
					DurationInMinutes = model.DurationInMinutes,
					HealthyStatus = model.HealthyStatus,
					CreatedOn = DateTime.UtcNow
				};
				_context.Recipes.Add(recipe);
								
				foreach(var ingredient in model.Ingredients)
				{
					var ing = new Ingredient
					{
						Recipe = recipe,
						Name = ingredient.Name,
						Quantity = ingredient.Quantity,
						UnityOfMeasure = ingredient.UnityOfMeasure,
						CreatedOn = DateTime.UtcNow
					};
					_context.Ingredients.Add(ing);

					// Add relation
					_context.RecipeIngredients.Add(new RecipeIngredient
					{
						Recipe = recipe,
						Ingredient = ing
					});
				}

				return await _context.SaveChangesAsync() > 0;
			}

			return false;
		}
	}
}
