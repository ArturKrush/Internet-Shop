using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EfCoreExamples.ContextV1.Entities;

namespace EfCoreExamples.ContextV1.EntityConfigurations
{
	public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
	{
		public void Configure(EntityTypeBuilder<Manufacturer> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(20);
			builder.Property(x => x.Description).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(100);
			builder.Property(x => x.FoundedDate).IsRequired().HasColumnType("DATETIME");

			builder.HasMany(x => x.Products)
				.WithOne(y => y.Manufacturer);
		}
	}
}
