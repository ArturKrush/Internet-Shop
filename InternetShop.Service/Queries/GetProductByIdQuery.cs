using InternetShop.Contract.Responses;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<long, ProductResponse?>
    {
        private readonly InternetShopDbContext _context;

        public GetProductByIdQueryHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse?> Handle(long productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.Id == productId)
                .Select(x => new ProductResponse
                {
                    ProductId = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ManufacturerId = x.ManufacturerId,
                    ManufacturerName = x.Manufacturer.Name,
                    CategoriesOfProduct = x.Categories
                        .Select(c => new CategoryBriefResponse
                        {
                            CategoryId = c.Id,
                            CategoryName = c.Name
                        }
                        ).ToList(),
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
