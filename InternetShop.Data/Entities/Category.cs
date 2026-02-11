namespace EfCoreExamples.ContextV1.Entities
{
    public record Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual IEnumerable<Product> Products { get; set; } = [];
    }
}
