using InternetShop.Data;
using InternetShop.Contract.Responses;
using Microsoft.EntityFrameworkCore;


namespace InternetShop.Service.Queries
{
    public class GetCategoriesQueryHandler : IRequestHandler<IList<CategoryResponse>>
    {
        private readonly InternetShopDbContext _context;

        public GetCategoriesQueryHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(x => new CategoryResponse
                {
                    CategoryId = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .OrderByDescending(x => x.CategoryId)
                .ToListAsync(cancellationToken);
        }
    }
}
