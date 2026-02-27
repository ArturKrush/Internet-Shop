namespace InternetShop.Contract.Responses
{
    public class CustomerResponse
    {
        public long CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
