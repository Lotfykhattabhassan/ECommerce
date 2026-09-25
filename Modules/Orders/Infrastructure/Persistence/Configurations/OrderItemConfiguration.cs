using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerce.Modules.Orders.Domain.Entities;

namespace MiniECommerce.Modules.Orders.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasIndex(x => x.OrderId);

            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => new
            {
                x.OrderId,
                x.ProductId
            }).IsUnique();
        }
    }
}