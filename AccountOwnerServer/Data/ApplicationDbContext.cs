using AccountOwnerServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOwnerServer.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ingredient>()
				.HasOne(i => i.Recipe)
				.WithMany(r => r.Ingredients);

			modelBuilder.Entity<RecipeIngredient>()
				.HasKey(ri => new { ri.RecipeId, ri.IngredientId });
		}
	}
}
