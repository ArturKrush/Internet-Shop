namespace InternetShop.Service.Commands
{
    public class UpdateCustomerCommand
    {
        public long CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
