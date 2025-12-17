using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class PersonalProfileRepository : IPersonalProfileRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<PersonalProfile> _profiles;

    public PersonalProfileRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _profiles = _dbContext.Set<PersonalProfile>();
    }

    public IQueryable<PersonalProfile> Query()
        => _profiles.AsQueryable();

    public async Task<PersonalProfile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _profiles
            .FirstOrDefaultAsync(e => e.Id!.Equals(id), cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<PersonalProfile>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        return await _profiles
            .AsNoTracking()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<PersonalProfile, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _profiles
            .AnyAsync(predicate, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task AddAsync(
        PersonalProfile entity,
        CancellationToken cancellationToken = default)
    {
        await _profiles.AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);
    }

    public Task Update(
        PersonalProfile entity,
        CancellationToken cancellationToken = default)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
            _profiles.Attach(entity);

        _dbContext.Entry(entity).State = EntityState.Modified;

        return Task.CompletedTask;
    }
}
