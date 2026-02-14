using InternetShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Data
{
	public class InternetShopDbContext : DbContext
	{
		public InternetShopDbContext(DbContextOptions<InternetShopDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<OrderLine> Orders { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Manufacturer> Manufactures { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(InternetShopDbContext).Assembly);
			new DbInitializer(modelBuilder).Seed();
		}
	}
}
