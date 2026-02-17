using InternetShop.Data;
using InternetShop.Contract.Responses;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Queries
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<long, CategoryResponse?>
    {
        private readonly InternetShopDbContext _context;

        public GetCategoryByIdQueryHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse?> Handle(long categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.Id == categoryId)
                .Select(x => new CategoryResponse
                {
                    CategoryId = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ProductsOfCategory = x.Products
                        .Select(p => new ProductResponse
                        {
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            ManufacturerName = p.Manufacturer.Name
                        })
                        .ToList()
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
