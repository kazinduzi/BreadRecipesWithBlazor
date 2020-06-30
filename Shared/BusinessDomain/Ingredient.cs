using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Shared.BusinessDomain
{
	public class Ingredient : BaseEntity
	{
		[StringLength(255), Required]
		public string Name { get; set; }

		[StringLength(100)]
		public string Amount { get; set; }

		public int RecipeId { get; set; }

		public Recipe Recipe { get; set; }
	}
}
