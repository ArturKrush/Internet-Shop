namespace InternetShop.Contract.Responses
{
    public class OrderResponse
    {
        public long OrderId { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderLineResponse>? OrderLines { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
