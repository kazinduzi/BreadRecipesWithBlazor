using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOwnerServer.Models
{
	public enum HealthyStatus
	{
		Healthy,
		NotHealthy
	}
	public class Recipe
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public HealthyStatus healthyStatus { get; set; } = HealthyStatus.Healthy;
		public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}
}
