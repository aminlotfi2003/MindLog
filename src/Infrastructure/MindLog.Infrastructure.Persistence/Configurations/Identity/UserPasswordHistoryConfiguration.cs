using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Configurations.Identity;

public sealed class UserPasswordHistoryConfiguration : IEntityTypeConfiguration<UserPasswordHistory>
{
    public void Configure(EntityTypeBuilder<UserPasswordHistory> builder)
    {
        builder.ToTable("UserPasswordHistories", Schemas.Identity);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PasswordHash).IsRequired();

        builder.HasIndex(x => new { x.UserId, x.ChangedAt });

        builder.HasOne(x => x.User)
            .WithMany(u => u.PasswordHistories)
            .HasForeignKey(x => x.UserId);
    }
}
