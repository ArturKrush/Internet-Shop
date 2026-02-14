namespace InternetShop.Data.Entities
{
    public record Manufacturer : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime FoundedDate { get; set; }

        public virtual IEnumerable<Product> Products { get; set; } = [];
    }
}
