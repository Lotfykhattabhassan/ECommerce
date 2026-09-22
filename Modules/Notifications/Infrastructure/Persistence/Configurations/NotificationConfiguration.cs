using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerce.Modules.Notifications.Domain.Entities;

namespace MiniECommerce.Modules.Notifications.Infrastructure.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).IsRequired()
                .HasMaxLength(1000);
            builder.Property(x => x.Title).IsRequired()
                .HasMaxLength(200);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
