namespace InternetShop.Contract.Responses
{
    public class OrderLineResponse
    {
        public long OrderLineId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public long ProductId { get; set; }

        public string? ProductName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
