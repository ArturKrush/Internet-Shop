using EfCoreExamples.ContextV1.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreExamples.ContextV1
{
	public class DbInitializer
	{
		private readonly ModelBuilder _modelBuilder;

		public DbInitializer(ModelBuilder modelBuilder)
		{
			_modelBuilder = modelBuilder;
		}

		public void Seed()
		{
			_modelBuilder.Entity<Category>(x =>
			{
				x.HasData(new Category
				{
					Id = 1,
					Name = "Mobile Phones",
					Description = "SmartPhones"
				});
				x.HasData(new Category
				{
					Id = 2,
					Name = "Tablets",
					Description = "Modern tablets"
				});
				x.HasData(new Category
				{
					Id = 3,
					Name = "Laptops",
					Description = "Modern laptops"
				});
			});
		}
	}
}
