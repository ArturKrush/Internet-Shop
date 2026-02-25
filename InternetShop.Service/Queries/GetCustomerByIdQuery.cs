using InternetShop.Contract.Responses;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<long, CustomerResponse?>
    {
        private readonly InternetShopDbContext _context;

        public GetCustomerByIdQueryHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponse?> Handle(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.Id == customerId)
                .Select(x => new CustomerResponse
                {
                    CustomerId = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    //CategoriesOfProduct = x.Categories
                    //    .Select(c => new CategoryBriefResponse
                    //    {
                    //        CategoryId = c.Id,
                    //        CategoryName = c.Name
                    //    }
                    //    ).ToList(),
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}