using AccountOwnerServer.Data;
using AccountOwnerServer.Dtos;
using AccountOwnerServer.Models;
using AccountOwnerServer.ViewModels;
using Microsoft.AspNetCore.Http;
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

		[HttpGet("{id:int}")]
		public async Task<ActionResult<RecipeViewModel>> GetById(int id)
		{
			try
			{
				var recipe = await _context.Recipes.AsNoTracking()
				.Include(r => r.RecipeIngredients)
				.Where(r => r.Id == id)
				.Select(r => new RecipeViewModel
				{
					Id = r.Id,
					Name = r.Name,
					CreatedOn = r.CreatedOn,
					UpdatedOn = r.UpdatedOn,
					DurationInMinutes = r.DurationInMinutes,
					Ingredients = r.RecipeIngredients.Select(ri => new IngredientViewModel
					{
						Id = ri.IngredientId,
						Name = ri.Ingredient.Name,
						Quantity = ri.Ingredient.Quantity,
						UnityOfMeasure = ri.Ingredient.UnityOfMeasure
					})
				}).FirstOrDefaultAsync();

				if (recipe == null)
				{
					return NotFound(new { message = $"Recipe with id {id} could not be found!" });
				}

				return Ok(recipe);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error retrieving data from the database");
			}
			
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
