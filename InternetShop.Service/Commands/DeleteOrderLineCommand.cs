namespace InternetShop.Service.Commands
{
    public class DeleteOrderLineCommand
    {
        public long OrderLineId { get; set; }

        public long OrderId { get; set; }

        public long CustomerId { get; set; }
    }
}
