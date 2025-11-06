using SharedLibrary.Constants;
using System.Collections.Generic;

namespace BreadRecipesWithWasmBlazor.Client.ViewModels
{
 public class RecipeInputModel
 {
 public string Name { get; set; }
 public int DurationInMinutes { get; set; }
 public HealthyStatus HealthyStatus { get; set; }
 public List<IngredientInputModel> Ingredients { get; set; } = new List<IngredientInputModel>();
 }
}
