using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountOwnerServer.Models;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Constants;

namespace AccountOwnerServer.Data
{
    public class AppSeeder
    {
        private readonly ApplicationDbContext _context;

        public AppSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Ensure database exists/migrated - caller already migrates
            if (await _context.Recipes.AsNoTracking().AnyAsync())
            {
                return;
            }

            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Sourdough Bread",
                    DurationInMinutes = 240,
                    HealthyStatus = HealthyStatus.Healthy,
                    CreatedOn = DateTime.UtcNow
                },
                new Recipe
                {
                    Name = "Whole Wheat Loaf",
                    DurationInMinutes = 120,
                    HealthyStatus = HealthyStatus.Healthy,
                    CreatedOn = DateTime.UtcNow
                },
                new Recipe
                {
                    Name = "Cinnamon Rolls",
                    DurationInMinutes = 150,
                    HealthyStatus = HealthyStatus.NotHealthy,
                    CreatedOn = DateTime.UtcNow
                }
            };

            foreach (var recipe in recipes)
            {
                _context.Recipes.Add(recipe);

                var seedIngredients = GetSeedIngredientsFor(recipe.Name);
                foreach (var ing in seedIngredients)
                {
                    ing.Recipe = recipe;
                    _context.Ingredients.Add(ing);
                    _context.RecipeIngredients.Add(new RecipeIngredient
                    {
                        Recipe = recipe,
                        Ingredient = ing
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        private static IEnumerable<Ingredient> GetSeedIngredientsFor(string recipeName)
        {
            switch (recipeName)
            {
                case "Sourdough Bread":
                    return new[]
                    {
                        CreateIng("Bread Flour", 500, "g"),
                        CreateIng("Water", 350, "ml"),
                        CreateIng("Salt", 10, "g"),
                        CreateIng("Sourdough Starter", 100, "g")
                    };
                case "Whole Wheat Loaf":
                    return new[]
                    {
                        CreateIng("Whole Wheat Flour", 450, "g"),
                        CreateIng("AP Flour", 150, "g"),
                        CreateIng("Water", 380, "ml"),
                        CreateIng("Salt", 8, "g"),
                        CreateIng("Yeast", 7, "g")
                    };
                case "Cinnamon Rolls":
                    return new[]
                    {
                        CreateIng("AP Flour", 500, "g"),
                        CreateIng("Milk", 250, "ml"),
                        CreateIng("Sugar", 50, "g"),
                        CreateIng("Butter", 60, "g"),
                        CreateIng("Cinnamon", 15, "g")
                    };
                default:
                    return Array.Empty<Ingredient>();
            }
        }

        private static Ingredient CreateIng(string name, int qty, string unit)
        {
            return new Ingredient
            {
                Name = name,
                Quantity = qty,
                UnityOfMeasure = unit,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}


