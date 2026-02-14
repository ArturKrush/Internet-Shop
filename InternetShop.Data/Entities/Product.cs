namespace InternetShop.Data.Entities
{
    public record Product : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; } = [];

		public virtual IEnumerable<OrderLine> OrderLines { get; set; } = [];
	}
}
