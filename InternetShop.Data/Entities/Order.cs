namespace InternetShop.Data.Entities
{
    public record Order : BaseEntity
    {
        public int CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual IEnumerable<OrderLine> OrderLines { get; set; } = [];
    }
}
