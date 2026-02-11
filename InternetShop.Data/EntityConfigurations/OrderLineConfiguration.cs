using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EfCoreExamples.ContextV1.Entities;

namespace EfCoreExamples.ContextV1.EntityConfigurations
{
	public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
	{
		public void Configure(EntityTypeBuilder<OrderLine> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.ProductId).IsRequired();
			builder.Property(x => x.CustomerId).IsRequired();

			builder.HasOne(d => d.Product)
				.WithMany(p => p.Orders)
				.HasForeignKey(d => d.ProductId)
				.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasOne(d => d.Customer)
				.WithMany(p => p.Orders)
				.HasForeignKey(d => d.CustomerId)
				.OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
