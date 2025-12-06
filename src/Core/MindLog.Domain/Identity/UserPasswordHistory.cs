using MindLog.Domain.Common;

namespace MindLog.Domain.Identity;

public class UserPasswordHistory : EntityBase<Guid>
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public string PasswordHash { get; set; } = default!;
    public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.UtcNow;

    public static UserPasswordHistory Create(Guid userId, string passwordHash, DateTimeOffset changedAt)
        => new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PasswordHash = passwordHash,
            ChangedAt = changedAt
        };
}
