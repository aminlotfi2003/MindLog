using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Common;

public abstract class EntityBase<TId> : IEntity<TId>
{
    public TId Id { get; set; } = default!;
}
