using InternetShop.Data;
using InternetShop.Contract.Responses;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly InternetShopDbContext _context;

        public CreateCategoryCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = request.InsertCategory();
            await _context.AddAsync(category, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CategoryResponse
            {
                CategoryId = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
