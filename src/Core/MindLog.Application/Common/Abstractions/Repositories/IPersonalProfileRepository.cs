using MindLog.Domain.Entities;
using System.Linq.Expressions;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IPersonalProfileRepository
{
    IQueryable<PersonalProfile> Query();

    Task<PersonalProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PersonalProfile>> ListAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<PersonalProfile, bool>> predicate, CancellationToken cancellationToken = default);

    Task AddAsync(PersonalProfile profile, CancellationToken cancellationToken = default);

    Task Update(PersonalProfile profile, CancellationToken cancellationToken = default);
}
