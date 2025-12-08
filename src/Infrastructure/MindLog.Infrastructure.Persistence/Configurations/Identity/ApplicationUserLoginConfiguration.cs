using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Configurations.Identity;

public sealed class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        builder.ToTable("UserLogins", Schemas.Identity);

        builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });
    }
}
