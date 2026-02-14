namespace InternetShop.Data.Entities
{
    public record Customer : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; } = [];
    }
}
