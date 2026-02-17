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

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if (category == null)
                throw new Exception("Category to update is not exists");

            if(request.Name != null)
                category.Name = request.Name;
            if (request.Description != null)
                category.Description = request.Description;

            if(request.Name == null && request.Description == null)
                throw new Exception("No fields to update");
            //category.Name = category.Name != request.Name ? request.Name : category.Name;
            //category.Description = category.Description != request.Description ? request.Description : category.Description;

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
