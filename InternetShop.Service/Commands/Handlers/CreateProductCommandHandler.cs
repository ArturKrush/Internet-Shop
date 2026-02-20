using InternetShop.Contract.Responses;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly InternetShopDbContext _context;

        public CreateProductCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = request.InsertProduct();
            if (request.CategoriesOfProduct != null && request.CategoriesOfProduct.Any())
            {
                // З БД беруться усі категорії, ID яких вказані у переданому списку ID'шників
                var existingCategories = await _context.Categories
                    .Where(c => request.CategoriesOfProduct.Contains(c.Id))
                    .ToListAsync(cancellationToken);

                // Перевірка чи всі ID наявні в базі
                if (existingCategories.Count != request.CategoriesOfProduct.Count)
                {
                    throw new InvalidOperationException("One or more categories were not found");
                }

                // Наявні у БД категорії додаються до продукту
                product.Categories = existingCategories;
            }

            // Збереження змін
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken); // Тут генерується product.Id

            product.Manufacturer = await _context.Manufactures.SingleOrDefaultAsync(m => m.Id == product.ManufacturerId);

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
                    ).ToList()
            };
        }
    }
}
