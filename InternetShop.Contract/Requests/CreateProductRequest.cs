using System.ComponentModel.DataAnnotations;

namespace InternetShop.Contract.Requests
{
    public class CreateProductRequest
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public long ManufacturerId { get; set; }

        public List<long>? CategoriesOfProduct { get; set; }
    }
}
