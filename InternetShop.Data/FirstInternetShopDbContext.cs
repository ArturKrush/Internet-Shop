using EfCoreExamples.ContextV1.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreExamples.ContextV1
{
	public class FirstInternetShopDbContext : DbContext
	{
		public FirstInternetShopDbContext(DbContextOptions<FirstInternetShopDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<OrderLine> Orders { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Manufacturer> Manufactures { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(FirstInternetShopDbContext).Assembly);
			new DbInitializer(modelBuilder).Seed();
		}
	}
}
