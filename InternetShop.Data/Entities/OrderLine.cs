namespace InternetShop.Data.Entities
{
    public class OrderLine : BaseEntity
    {
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public long ProductId { get; set; }

        public long OrderId { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Order? Order { get; set; }
    }
}
