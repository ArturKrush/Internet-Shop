using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InternetShop.Data.Entities;

namespace InternetShop.Data.EntityConfigurations
{
	public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
	{
		public void Configure(EntityTypeBuilder<OrderLine> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("DECIMAL(18, 2)");
            builder.Property(x => x.Quantity).IsRequired().HasColumnType("INT");

            builder.HasOne(d => d.Product)
				.WithMany(p => p.OrderLines)
				.HasForeignKey(d => d.ProductId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(d => d.Order)
				.WithMany(p => p.OrderLines)
				.HasForeignKey(d => d.OrderId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
