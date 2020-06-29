using BlazorAppWASM.Shared.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Shared.BusinessDomain
{
	public class Recipe : BaseEntity
	{
		[StringLength(255), Required]
		public string Name { get; set; }

		public int Duration { get; set; }

		public HealthyLabel HealthyLabel { get; set; }

		public IEnumerable<Ingredient> Ingredients { get; set; }
	}
}
