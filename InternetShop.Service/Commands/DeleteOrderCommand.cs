namespace InternetShop.Service.Commands
{
    public class DeleteOrderCommand
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }
    }
}
