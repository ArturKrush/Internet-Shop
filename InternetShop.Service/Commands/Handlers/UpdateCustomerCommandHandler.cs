using Microsoft.EntityFrameworkCore;
using InternetShop.Contract.Responses;
using InternetShop.Data;
using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly InternetShopDbContext _context;

        public UpdateCustomerCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer == null)
                return null;

            //Якщо певне з полів прийшло порожнім, значить воно не оновлюється
            if (request.FirstName != null)
            {
                customer.FirstName = request.FirstName;
            }

            if (request.LastName != null)
            {
                customer.LastName = request.LastName;
            }

            if (request.BirthDate != null)
            {
                customer.BirthDate = request.BirthDate.Value;
            }

            // Якщо дійсно були зміни, то тільки тоді EntityFramework їх зберігає 
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

        private async Task<Customer?> GetCustomerAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == customerId, cancellationToken);
        }
    }
}
