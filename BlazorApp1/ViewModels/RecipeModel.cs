using System;
using System.Collections.Generic;

using SharedLibrary.Constants;

namespace BreadRecipesWithWasmBlazor.Client.ViewModels
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HealthyStatus HealthyStatus { get; set; }
        public int DurationInMinutes { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; } = new List<IngredientModel>();
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
