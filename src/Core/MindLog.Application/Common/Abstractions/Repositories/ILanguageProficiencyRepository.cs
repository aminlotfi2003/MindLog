using MindLog.Domain.Entities;
using System.Linq.Expressions;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface ILanguageProficiencyRepository
{
    IQueryable<LanguageProficiency> Query();

    Task<LanguageProficiency?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LanguageProficiency>> ListAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<LanguageProficiency, bool>> predicate, CancellationToken cancellationToken = default);

    Task AddAsync(LanguageProficiency profile, CancellationToken cancellationToken = default);

    Task Update(LanguageProficiency profile, CancellationToken cancellationToken = default);
}
