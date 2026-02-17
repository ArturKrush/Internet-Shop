using System.ComponentModel.DataAnnotations;

namespace InternetShop.Contract.Requests
{
    public class CreateCategoryRequest
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
