using InternetShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace InternetShop.Data
{
	public class InternetShopDbContext : DbContext
	{
		public InternetShopDbContext(DbContextOptions<InternetShopDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderLine> OrderLines { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Manufacturer> Manufactures { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Вибираються всі сутності, що наслідуються від Auditable і були змінені або додані
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                //Якщо сутність додано, то оновлюються обидва поля
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                //Якщо оновлено, то тільки UpdatedAt
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InternetShopDbContext).Assembly);
			new DbInitializer(modelBuilder).Seed();
		}
	}
}
