
namespace InternetShop.Service.Commands
{
    public class UpdateProductCommand
    {
        public long ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public long? ManufacturerId { get; set; }

        public List<long>? CategoriesOfProduct { get; set; }
    }
}
