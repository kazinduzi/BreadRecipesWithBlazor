using SharedLibrary.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountOwnerServer.Dtos
{
	public class RecipeInputModel
	{
		[Required(ErrorMessage ="Name recipe is required")]
		public string Name { get; set; }
		public int DurationInMinutes { get; set; }
		public HealthyStatus HealthyStatus { get; set; }
		public IList<IngredientInputModel> Ingredients { get; set; } = new List<IngredientInputModel>();
	}
}
