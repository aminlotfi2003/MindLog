namespace MindLog.SharedKernel.Abstractions;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? ModifiedAt { get; set; }
}
