using Microsoft.EntityFrameworkCore;
using MindLog.Infrastructure.Persistence.Contexts;
using MindLog.SharedKernel.Abstractions;
using System.Linq.Expressions;

namespace MindLog.Infrastructure.Persistence.Repositories;

public class EfRepository<TId, TEntity> : IRepository<TId, TEntity>
    where TEntity : class, IEntity<TId>, ISoftDeletable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public EfRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> Query()
        => _dbSet.AsQueryable();

    public async Task<TEntity?> GetByIdAsync(
        TId id,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<TEntity>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(predicate, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);
    }

    public Task Update(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

        _dbContext.Entry(entity).State = EntityState.Modified;

        return Task.CompletedTask;
    }

    public Task Remove(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        // Soft Delete
        entity.IsDeleted = true;

        if (_dbContext.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

        _dbContext.Entry(entity).Property(e => e.IsDeleted).IsModified = true;

        return Task.CompletedTask;
    }

    public Task Restore(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = false;

        if (_dbContext.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

        _dbContext.Entry(entity).Property(e => e.IsDeleted).IsModified = true;

        return Task.CompletedTask;
    }
}
