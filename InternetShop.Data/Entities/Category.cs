namespace InternetShop.Data.Entities
{
    public record Category : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public virtual IEnumerable<Product> Products { get; set; } = [];
    }
}
