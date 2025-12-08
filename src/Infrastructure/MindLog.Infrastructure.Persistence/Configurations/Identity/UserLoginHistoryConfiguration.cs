using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Configurations.Identity;

public sealed class UserLoginHistoryConfiguration : IEntityTypeConfiguration<UserLoginHistory>
{
    public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
    {
        builder.ToTable("UserLoginHistories", Schemas.Identity);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(64);

        builder.Property(x => x.Host)
            .HasMaxLength(255);

        builder.HasIndex(x => new { x.UserId, x.OccurredAt });

        builder.HasOne(x => x.User)
            .WithMany(u => u.LoginHistories)
            .HasForeignKey(x => x.UserId);
    }
}
