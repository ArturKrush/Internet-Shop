using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Entities;

namespace InternetShop.Data.EntityConfigurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("DECIMAL(18, 2)");
			builder.Property(x => x.Status).HasColumnType("NVARCHAR(60)").HasConversion<string>();

            builder.HasOne(d => d.Customer)
				.WithMany(p => p.Orders)
				.HasForeignKey(d => d.CustomerId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasMany(d => d.OrderLines)
				.WithOne(p => p.Order);
		}
	}
}
