using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerce.Modules.Cart.Domain.Entities;

namespace MiniECommerce.Modules.Cart.Infrastructure.Persistence.Configurations
{
    public class CartConfiguration
        : IEntityTypeConfiguration<Domain.Entities.Cart>
    {
        void IEntityTypeConfiguration<Domain.Entities.Cart>.Configure(EntityTypeBuilder<Domain.Entities.Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId).IsUnique(false);

            builder.HasMany(x => x.CartItems)
                .WithOne(x=>x.Cart)
                .HasForeignKey(x => x.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
