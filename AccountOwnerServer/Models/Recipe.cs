using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOwnerServer.Models
{
	public class Recipe
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int DurationInMinutes { get; set; }
		public HealthyStatus HealthyStatus { get; set; } = HealthyStatus.Healthy;
		public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}
}
