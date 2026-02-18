using Microsoft.EntityFrameworkCore;
using InternetShop.Contract.Responses;
using InternetShop.Data;
using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly InternetShopDbContext _context;

        public UpdateCategoryCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if (category == null)
                return null;

            //Якщо певне з полів прийшло порожнім, значить воно не оновлюється
            if (request.Name != null)
            {
                category.Name = request.Name;
            }

            if (request.Description != null)
            {
                category.Description = request.Description;
            }

            // Якщо дійсно були зміни, то тільки тоді EntityFramework їх зберігає 
            await _context.SaveChangesAsync(cancellationToken);

            return new CategoryResponse
            {
                CategoryId = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        private async Task<Category> GetCategoryAsync(long categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        }
    }
}
