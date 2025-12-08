using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Configurations.Identity;

public sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable("UserRoles", Schemas.Identity);

        builder.HasKey(x => new { x.UserId, x.RoleId });
    }
}
