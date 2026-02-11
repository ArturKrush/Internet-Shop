using EfCoreExamples.ContextV1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreExamples.ContextV1.EntityConfigurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(20);
			builder.Property(x => x.Description).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(100);

			builder.HasMany(x => x.Products)
				.WithMany(y => y.Categories);
		}
	}
}
