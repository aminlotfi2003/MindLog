using Microsoft.AspNetCore.Identity;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Identity;

public class ApplicationRole : IdentityRole<Guid>, IAuditableEntity
{
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
}
