using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class LanguageProficiencyRepository : ILanguageProficiencyRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<LanguageProficiency> _languages;

    public LanguageProficiencyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _languages = _dbContext.Set<LanguageProficiency>();
    }

    public IQueryable<LanguageProficiency> Query()
        => _languages.AsQueryable();

    public async Task<LanguageProficiency?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _languages
            .FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<LanguageProficiency>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        return await _languages
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<LanguageProficiency, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _languages
            .AnyAsync(predicate, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task AddAsync(
        LanguageProficiency entity,
        CancellationToken cancellationToken = default)
    {
        await _languages.AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);
    }

    public Task Update(
        LanguageProficiency entity,
        CancellationToken cancellationToken = default)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
            _languages.Attach(entity);

        _dbContext.Entry(entity).State = EntityState.Modified;

        return Task.CompletedTask;
    }
}
