using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Entities;

namespace InternetShop.Data
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
            // Заповнення таблиці Category
            _modelBuilder.Entity<Category>(x =>
            {
                x.HasData(new Category
                {
                    Id = 1,
                    Name = "Mobile Phones",
                    Description = "Smartphones"
                });
                x.HasData(new Category
                {
                    Id = 2,
                    Name = "Windows",
                    Description = "Operation system"
                });
                x.HasData(new Category
                {
                    Id = 3,
                    Name = "Laptops",
                    Description = "Modern laptops"
                });
            });
            // Заповнення таблиці Customer
            _modelBuilder.Entity<Customer>(x =>
            {
                x.HasData(new Customer
                {
                    Id = 1,
                    FirstName = "Pavlo",
                    LastName = "Kushnirenko",
                    BirthDate = new DateTime(1990, 12, 15)
                });
                x.HasData(new Customer
                {
                    Id = 2,
                    FirstName = "Olena",
                    LastName = "Petrenko",
                    BirthDate = new DateTime(1970, 3, 20)
                });
                x.HasData(new Customer
                {
                    Id = 3,
                    FirstName = "Oleg",
                    LastName = "Ganev",
                    BirthDate = new DateTime(2001, 5, 17)
                });
            });
            // Заповнення таблиці Manufacturer
            _modelBuilder.Entity<Manufacturer>(x =>
            {
                x.HasData(new Manufacturer
                {
                    Id = 1,
                    Name = "Apple",
                    Description = "Smartphones, laptops, tablets, watches, software, hardware",
                    FoundedDate = new DateTime(1976, 1, 17)
                });
                x.HasData(new Manufacturer
                {
                    Id = 2,
                    Name = "Lenovo",
                    Description = "Smartphones, laptops, tablets, watches, software, hardware",
                    FoundedDate = new DateTime(1984, 10, 1)
                });
                x.HasData(new Manufacturer
                {
                    Id = 3,
                    Name = "Samsung",
                    Description = "Smartphones, tablets, watches, software",
                    FoundedDate = new DateTime(1969, 1, 13)
                });
            });
            // Заповнення таблиці Product
            _modelBuilder.Entity<Product>(x =>
            {
                x.HasData(new Product
                {
                    Id = 1,
                    Name = "Iphone 17 Pro Max",
                    Description = "Better camera, CPU, more RAM than in 16 Pro Max",
                    Price = 20000,
                    ManufacturerId = 1,
                });
                x.HasData(new Product
                {
                    Id = 2,
                    Name = "MacBook Air M4",
                    Description = "Expensive, but productive laptop for funs",
                    Price = 35000,
                    ManufacturerId = 1
                });
                x.HasData(new Product
                {
                    Id = 3,
                    Name = "Lenovo LOQ",
                    Description = "Powerfull laptop, known for high quality cooling system",
                    Price = 30000,
                    ManufacturerId = 2
                });
                x.HasData(new Product
                {
                    Id = 4,
                    Name = "Samsung S24A",
                    Description = "Elegant smartphone with fine camera",
                    Price = 17550,
                    ManufacturerId = 3
                });
            });
            // Заповнення таблиці Order
            _modelBuilder.Entity<Order>(x =>
            {
                x.HasData(new Order
                {
                    Id = 1,
                    CustomerId = 2,
                    TotalPrice = 75000
                });
                x.HasData(new Order
                {
                    Id = 2,
                    CustomerId = 3,
                    TotalPrice = 30000
                });
                x.HasData(new Order
                {
                    Id = 3,
                    CustomerId = 1,
                    TotalPrice = 47550
                });
            });
            // Заповнення таблиці OrderLine
            _modelBuilder.Entity<OrderLine>(x =>
            {
                x.HasData(new OrderLine
                {
                    Id = 1,
                    ProductId = 1,
                    OrderId = 1,
                    TotalPrice = 40000,
                    Quantity = 2
                });
                x.HasData(new OrderLine
                {
                    Id = 2,
                    ProductId = 2,
                    OrderId = 1,
                    TotalPrice = 35000,
                    Quantity = 1
                });
                x.HasData(new OrderLine
                {
                    Id = 3,
                    ProductId = 3,
                    OrderId = 2,
                    TotalPrice = 30000,
                    Quantity = 1
                });
                x.HasData(new OrderLine
                {
                    Id = 4,
                    ProductId = 3,
                    OrderId = 3,
                    TotalPrice = 30000,
                    Quantity = 1
                });
                x.HasData(new OrderLine
                {
                    Id = 5,
                    ProductId = 4,
                    OrderId = 3,
                    TotalPrice = 17550,
                    Quantity = 1
                });
            });
            // Заповнення таблиці ProductCategory
            _modelBuilder.Entity<ProductCategory>(x =>
            {
                x.HasData(new ProductCategory
                {
                    ProductId = 1,
                    CategoryId = 1
                });
                x.HasData(new ProductCategory
                {
                    ProductId = 2,
                    CategoryId = 3
                });
                x.HasData(new ProductCategory
                {
                    ProductId = 3,
                    CategoryId = 2
                });
                x.HasData(new ProductCategory
                {
                    ProductId = 3,
                    CategoryId = 3
                });
                x.HasData(new ProductCategory
                {
                    ProductId = 4,
                    CategoryId = 1
                });
            });
        }
    }
}
