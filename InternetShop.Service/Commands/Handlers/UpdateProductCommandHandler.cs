using Azure;
using InternetShop.Contract.Responses;
using InternetShop.Data;
using InternetShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly InternetShopDbContext _context;

        public UpdateProductCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product == null)
                return null;

            //Якщо певне з полів прийшло порожнім, значить воно не оновлюється
            if (request.Name != null)
            {
                product.Name = request.Name;
            }

            if (request.Description != null)
            {
                product.Description = request.Description;
            }

            if (request.Price != null)
            {
                product.Price = request.Price.Value;
            }

            if (request.ManufacturerId != null)
            {
                product.ManufacturerId = request.ManufacturerId.Value;
                product.Manufacturer = await _context.Manufactures
                    .SingleOrDefaultAsync(m => m.Id == product.ManufacturerId);
            }

            if (request.CategoriesOfProduct != null)
            {
                product.Categories.Clear();

                if (request.CategoriesOfProduct.Any())
                {
                    var existingCategories = await _context.Categories
                        .Where(c => request.CategoriesOfProduct.Contains(c.Id))
                        .ToListAsync(cancellationToken);

                    // Перевірка чи всі ID наявні в базі
                    if (existingCategories.Count != request.CategoriesOfProduct.Count)
                    {
                        throw new InvalidOperationException("One or more categories were not found");
                    }

                    // Нові категорії додаються до продукту
                    foreach (var category in existingCategories)
                    {
                        product.Categories.Add(category);
                    }
                }
            }

            // Якщо дійсно були зміни, то тільки тоді EntityFramework їх зберігає 
            await _context.SaveChangesAsync(cancellationToken);

            return new ProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ManufacturerId = product.ManufacturerId,
                ManufacturerName = product.Manufacturer.Name,
                CategoriesOfProduct = product.Categories
                    .Select(c => new CategoryBriefResponse
                    {
                        CategoryId = c.Id,
                        CategoryName = c.Name
                    }
                    ).ToList(),
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        private async Task<Product?> GetProductAsync(long productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.Categories)
                .Include(x => x.Manufacturer)
                .SingleOrDefaultAsync(x => x.Id == productId, cancellationToken);
        }
    }
}
