namespace EfCoreExamples.ContextV1.Entities
{
    public record Customer : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public virtual IEnumerable<OrderLine> Orders { get; set; } = [];
    }
}
