using InternetShop.Contract.Responses;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;


namespace InternetShop.Service.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<IList<CustomerResponse>>
    {
        private readonly InternetShopDbContext _context;

        public GetCustomersQueryHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CustomerResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
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
                .ToListAsync(cancellationToken);
        }
    }
}
