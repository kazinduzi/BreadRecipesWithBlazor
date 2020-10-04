namespace AccountOwnerServer.ViewModels
{
	public class IngredientViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public string UnityOfMeasure { get; set; }
		public int? RecipeId { get; set; }
		public RecipeViewModel Recipe { get; set; }
	}
}