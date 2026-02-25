using Microsoft.EntityFrameworkCore;
using InternetShop.Data;
using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly InternetShopDbContext _context;

        public DeleteCustomerCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken = default)
        {
            var customer = await GetCustomerAsync(request.CustomerId, cancellationToken);

            if (customer != null)
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Customer?> GetCustomerAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == customerId, cancellationToken);
        }
    }
}
