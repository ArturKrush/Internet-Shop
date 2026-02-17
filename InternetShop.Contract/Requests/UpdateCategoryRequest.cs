using System.ComponentModel.DataAnnotations;

namespace InternetShop.Contract.Requests
{
    public class UpdateCategoryRequest
    {
        [Required]
        public long CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
