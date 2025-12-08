using MindLog.Domain.Identity;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IUserLoginHistoryRepository
{
    Task<IReadOnlyList<UserLoginHistory>> GetRecentAsync(Guid userId, int count, CancellationToken cancellationToken = default);
    Task AddAsync(UserLoginHistory history, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
