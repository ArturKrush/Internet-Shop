using Microsoft.EntityFrameworkCore;
using InternetShop.Data;
using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly InternetShopDbContext _context;

        public DeleteCategoryCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Category> GetCategoryAsync(long categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        }
    }
}
