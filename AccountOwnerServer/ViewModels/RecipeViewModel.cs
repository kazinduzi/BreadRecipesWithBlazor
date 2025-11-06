using System;
using System.Collections.Generic;

using SharedLibrary.Constants;

namespace AccountOwnerServer.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HealthyStatus HealthyStatus { get; set; }
        public int DurationInMinutes { get; set; }
        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
