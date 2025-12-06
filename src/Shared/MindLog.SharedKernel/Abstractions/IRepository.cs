using System.Linq.Expressions;

namespace MindLog.SharedKernel.Abstractions;

public interface IRepository<TId, TEntity> where TEntity : IEntity<TId>
{
    IQueryable<TEntity> Query();

    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task Update(TEntity entity, CancellationToken cancellationToken = default);

    Task Remove(TEntity entity, CancellationToken cancellationToken = default);

    Task Restore(TEntity entity, CancellationToken cancellationToken = default);
}
