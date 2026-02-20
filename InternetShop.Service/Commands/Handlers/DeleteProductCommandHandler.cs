using Microsoft.EntityFrameworkCore;
using InternetShop.Data;
using InternetShop.Data.Entities;

namespace InternetShop.Service.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly InternetShopDbContext _context;

        public DeleteProductCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Product?> GetProductAsync(long productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .SingleOrDefaultAsync(x => x.Id == productId, cancellationToken);
        }
    }
}
