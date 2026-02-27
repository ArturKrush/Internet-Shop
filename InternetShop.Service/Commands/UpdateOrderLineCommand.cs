namespace InternetShop.Service.Commands
{
    public class UpdateOrderLineCommand
    {
        public long CustomerId { get; set; }

        public long OrderId { get; set; }

        public long OrderLineId { get; set; }

        public int Quantity { get; set; }
    }
}
