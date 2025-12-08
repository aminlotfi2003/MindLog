using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Identity;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Identity;

public class UserLoginHistoryRepository(ApplicationDbContext context) : IUserLoginHistoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IReadOnlyList<UserLoginHistory>> GetRecentAsync(
        Guid userId,
        int count,
        CancellationToken cancellationToken = default)
    {
        count = count <= 0 ? 10 : count;

        return await _context.UserLoginHistories
            .Where(history => history.UserId == userId)
            .OrderByDescending(history => history.OccurredAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UserLoginHistory history, CancellationToken cancellationToken = default)
    {
        await _context.UserLoginHistories.AddAsync(history, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
