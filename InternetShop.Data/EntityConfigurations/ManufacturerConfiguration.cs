using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Entities;

namespace InternetShop.Data.EntityConfigurations
{
	public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
	{
		public void Configure(EntityTypeBuilder<Manufacturer> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Name).IsRequired().HasColumnType("NVARCHAR(60)").HasMaxLength(60);
			builder.Property(x => x.Description).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(250);
			builder.Property(x => x.FoundedDate).IsRequired().HasColumnType("DATETIME2");

			builder.HasMany(x => x.Products)
				.WithOne(y => y.Manufacturer);
		}
	}
}
