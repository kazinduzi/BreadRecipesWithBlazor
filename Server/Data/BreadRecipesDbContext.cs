using BlazorAppWASM.Shared.BusinessDomain;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppWASM.Server.Data
{
	public class BreadRecipesDbContext : DbContext
	{
		public BreadRecipesDbContext(DbContextOptions<BreadRecipesDbContext> options) : base(options)
		{

		}
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<Recipe> Recipes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Recipe>()
				.HasMany(i => i.Ingredients)
				.WithOne(r => r.Recipe);
		}
	}
}
