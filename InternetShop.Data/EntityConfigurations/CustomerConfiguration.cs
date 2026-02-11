using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EfCoreExamples.ContextV1.Entities;

namespace EfCoreExamples.ContextV1.EntityConfigurations
{
	public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.FirstName).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(20);
			builder.Property(x => x.LastName).IsRequired().HasColumnType("NVARCHAR(250)").HasMaxLength(20);
			builder.Property(x => x.BirthDate).IsRequired().HasColumnType("DATETIME");

			builder.HasMany(x => x.Orders)
				.WithOne(y => y.Customer);
		}
	}
}
