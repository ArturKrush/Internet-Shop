
namespace InternetShop.Contract.Requests
{
    public class UpdateProductRequest
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public long? ManufacturerId { get; set; }

        public List<long>? CategoriesOfProduct { get; set; }
    }
}
