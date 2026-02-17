namespace InternetShop.Data.Entities
{
    public class Order : BaseEntity
    {
        public long CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; } = [];
    }
}
