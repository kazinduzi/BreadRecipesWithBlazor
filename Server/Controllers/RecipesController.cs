using BlazorAppWASM.Server.Data;
using BlazorAppWASM.Shared.BusinessDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BlazorAppWASM.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RecipesController : ControllerBase
	{
		private readonly IRecipeRepository _recipeRepository;
		private readonly ILogger<RecipesController> _logger;

		public RecipesController(IRecipeRepository recipeRepository, ILogger<RecipesController> logger)
		{
			_recipeRepository = recipeRepository;
			_logger = logger;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Recipe>> GetRecipes()
		{
			var recipes = _recipeRepository.GetRecipes();
			return Ok(recipes);
		}

		[HttpGet("{id}")]
		public ActionResult<Recipe> GetRecipeById(int id)
		{
			var recipe = _recipeRepository.GetRecipeById(id);
			if (recipe != null)
			{
				return Ok(recipe);
			}
			return NotFound();
		}
	}
}
