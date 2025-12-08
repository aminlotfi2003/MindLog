using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Configurations.Identity;

public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles", Schemas.Identity);

        builder.Property(x => x.Description)
            .HasMaxLength(256);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.ModifiedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(r => r.NormalizedName).IsUnique();
    }
}
