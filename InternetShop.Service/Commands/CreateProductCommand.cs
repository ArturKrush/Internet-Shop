using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands
{
    public class CreateProductCommand
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public long ManufacturerId { get; set; }

        public List<long>? CategoriesOfProduct { get; set; }

        public Product InsertProduct()
        {
            var product = new Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                ManufacturerId = ManufacturerId
            };

            return product;
        }
    }
}
