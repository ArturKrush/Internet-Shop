using System.ComponentModel.DataAnnotations;

namespace InternetShop.Contract.Requests
{
    public class CreateCustomerRequest
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
