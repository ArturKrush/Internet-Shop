namespace EfCoreExamples.ContextV1.Entities
{
    public record Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int ManufacturerId { get; set; }

        public int Counter { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public virtual IEnumerable<Category> Categories { get; set; } = [];

		public virtual IEnumerable<OrderLine> Orders { get; set; } = [];
	}
}
