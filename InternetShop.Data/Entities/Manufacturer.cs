namespace EfCoreExamples.ContextV1.Entities
{
    public record Manufacturer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime FoundedDate { get; set; }

        public virtual IEnumerable<Product> Products { get; set; } = [];
    }
}
