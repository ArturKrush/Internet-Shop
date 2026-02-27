using InternetShop.Contract.Responses;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly InternetShopDbContext _context;

        public CreateCustomerCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = request.InsertCustomer();
            await _context.AddAsync(customer, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CustomerResponse
            {
                CustomerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                BirthDate = customer.BirthDate,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }
    }
}
