using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Configurations.Identity;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users", Schemas.Identity);

        builder.Property(u => u.TenantId);

        builder.Property(u => u.PasswordLastChangedAt);

        builder.Property(u => u.MustChangePasswordOnNextLogin)
            .HasDefaultValue(false);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(u => u.ModifiedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(u => u.NormalizedUserName).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();

        builder.HasMany(x => x.RefreshTokens)
            .WithOne(t => t.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.PasswordHistories)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.LoginHistories)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
