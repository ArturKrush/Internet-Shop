using System.ComponentModel.DataAnnotations;

namespace InternetShop.Contract.Requests
{
    public class UpdateOrderLineRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
