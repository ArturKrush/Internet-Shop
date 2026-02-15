using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Entities;

namespace InternetShop.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasColumnType("NVARCHAR(60)").HasMaxLength(60);
            builder.Property(x => x.Description).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(250);
            builder.Property(x => x.Price).IsRequired().HasColumnType("DECIMAL(18, 2)");

            builder.HasMany(x => x.Categories)
                   .WithMany(y => y.Products)
                        .UsingEntity<ProductCategory>(
                            j => j.HasOne(pc => pc.Category)
                            // Залишаємо порожнім, якщо в Category немає колекції ICollection<ProductCategory>
                                  .WithMany()
                                  .HasForeignKey(pc => pc.CategoryId),
                            j => j.HasOne(pc => pc.Product)
                            // Залишаємо порожнім, якщо в Product немає колекції ICollection<ProductCategory>
                                  .WithMany()
                                  .HasForeignKey(pc => pc.ProductId),
                            j =>
                            {
                                // Налаштування композитного ключа
                                j.HasKey(pc => new { pc.ProductId, pc.CategoryId });
                                // Явно задаємо ім'я таблиці, щоб EF не створив ще одну зв'язуючу таблицю
                                j.ToTable("ProductCategories");
                            }
                        );

            builder.HasOne(x => x.Manufacturer)
                .WithMany(y => y.Products)
                .HasForeignKey(x => x.ManufacturerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
