using System.ComponentModel.DataAnnotations;

namespace InternetShop.Contract.Requests
{
    public class CreateOrderLineRequest
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public long ProductId { get; set; }
    }
}
