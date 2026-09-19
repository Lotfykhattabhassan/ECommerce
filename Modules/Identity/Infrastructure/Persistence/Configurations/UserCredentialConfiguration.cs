using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerce.Modules.Identity.Domain.Entities;
using MiniECommerce.Modules.Identity.Domain.Enums;

namespace MiniECommerce.Modules.Identity.Infrastructure.Persistence.Configurations
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.ToTable("UserCredentials");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.HasIndex(x => x.UserId).IsUnique();

            builder.Property(user => user.Status)
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<CredentialStatus>(value))
            .IsRequired();

            builder.HasOne(x => x.User)
                 .WithOne(x => x.Credential)
                 .HasForeignKey<UserCredential>(x => x.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
