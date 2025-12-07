using MindLog.Domain.Identity;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IUserRefreshTokenRepository
{
    Task AddAsync(UserRefreshToken token, CancellationToken cancellationToken = default);
    Task<UserRefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default);
    Task RevokeUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
