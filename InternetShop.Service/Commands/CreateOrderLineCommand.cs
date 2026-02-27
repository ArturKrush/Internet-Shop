//using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands
{
    public class CreateOrderLineCommand
    {
        public int Quantity { get; set; }

        public long ProductId { get; set; }

        public long CustomerId { get; set; }
    }
}
