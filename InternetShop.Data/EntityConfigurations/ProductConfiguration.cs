using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EfCoreExamples.ContextV1.Entities;

namespace EfCoreExamples.ContextV1.EntityConfigurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(20);
			builder.Property(x => x.Description).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(100);
			builder.Property(x => x.Price).IsRequired().HasColumnType("DECIMAL(18, 2)");

			builder.HasMany(x => x.Categories)
				.WithMany(y => y.Products);

			builder.HasOne(x => x.Manufacturer)
				.WithMany(y => y.Products)
				.HasForeignKey(x => x.ManufacturerId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
