using System;
using System.Collections.Generic;

namespace AccountOwnerServer.Models
{
	public class Ingredient
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public string UnityOfMeasure { get; set; }
		public int? RecipeId { get; set; }
		public virtual Recipe Recipe { get; set;}
		public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}
}
