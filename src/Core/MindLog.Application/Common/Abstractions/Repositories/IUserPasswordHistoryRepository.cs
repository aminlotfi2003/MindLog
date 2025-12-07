using MindLog.Domain.Identity;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IUserPasswordHistoryRepository
{
    Task<IReadOnlyList<UserPasswordHistory>> GetRecentAsync(Guid userId, int count, CancellationToken cancellationToken = default);
    Task AddAsync(UserPasswordHistory history, CancellationToken cancellationToken = default);
    Task PruneExcessAsync(Guid userId, int maxEntries, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
