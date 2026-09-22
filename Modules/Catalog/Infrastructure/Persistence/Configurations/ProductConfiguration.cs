using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerce.Modules.Catalog.Domain.Entities;

namespace MiniECommerce.Modules.Catalog.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SKU).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);
            builder.Property(x => x.CategoryId).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(1000);

            builder.HasIndex(x => x.SKU)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
