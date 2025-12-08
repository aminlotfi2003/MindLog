using Microsoft.AspNetCore.Identity;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    public Guid? TenantId { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }

    public DateTimeOffset? PasswordLastChangedAt { get; set; }
    public bool MustChangePasswordOnNextLogin { get; set; }

    public ICollection<UserLoginHistory> LoginHistories { get; set; } = new HashSet<UserLoginHistory>();
    public ICollection<UserPasswordHistory> PasswordHistories { get; set; } = new HashSet<UserPasswordHistory>();
    public ICollection<UserRefreshToken> RefreshTokens { get; set; } = new HashSet<UserRefreshToken>();
}
