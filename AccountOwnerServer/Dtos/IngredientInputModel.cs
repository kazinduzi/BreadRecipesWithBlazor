using System.ComponentModel.DataAnnotations;

namespace AccountOwnerServer.Dtos
{
	public class IngredientInputModel
	{
		[Required(ErrorMessage = "Ingredient name is required")]
		public string Name { get; set; }
		public int Quantity { get; set; }
		public string UnityOfMeasure { get; set; }
	}
}
