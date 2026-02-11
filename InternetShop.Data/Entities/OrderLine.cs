namespace EfCoreExamples.ContextV1.Entities
{
    public record OrderLine : BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
