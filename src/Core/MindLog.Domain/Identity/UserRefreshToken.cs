using MindLog.Domain.Common;
using System.Security.Cryptography;

namespace MindLog.Domain.Identity;

public class UserRefreshToken : EntityBase<Guid>
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public string TokenHash { get; set; } = default!;
    public DateTimeOffset ExpiresAt { get; set; }
    public bool Revoked { get; set; }

    private UserRefreshToken() { } // for EF

    public UserRefreshToken(Guid userId, string tokenHash, DateTimeOffset expiresAt)
    {
        UserId = userId;
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
        Revoked = false;
    }

    public static UserRefreshToken Issue(Guid userId, int days = 7)
        => new(userId,
               Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
               DateTimeOffset.UtcNow.AddDays(days)
        );

    public static UserRefreshToken CreateHashed(Guid userId, string tokenHash, DateTimeOffset expiresAt)
        => new(userId, tokenHash, expiresAt);

    public void Revoke() => Revoked = true;

    public bool IsExpired(DateTimeOffset utcNow) => ExpiresAt <= utcNow;

    public bool IsActive(DateTimeOffset utcNow) => !Revoked && !IsExpired(utcNow);
}
