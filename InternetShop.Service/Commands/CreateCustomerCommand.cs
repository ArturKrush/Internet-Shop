using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands
{
    public class CreateCustomerCommand
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Customer InsertCustomer()
        {
            Customer customer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate
            };

            return customer;
        }
    }
}
