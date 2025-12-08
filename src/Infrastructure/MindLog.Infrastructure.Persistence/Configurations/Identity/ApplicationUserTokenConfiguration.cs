using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Identity;

namespace MindLog.Infrastructure.Persistence.Configurations.Identity;

public sealed class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.ToTable("UserTokens", Schemas.Identity);

        builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
    }
}
