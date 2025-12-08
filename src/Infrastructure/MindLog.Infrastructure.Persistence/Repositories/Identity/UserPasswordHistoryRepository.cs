using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Identity;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Identity;

public class UserPasswordHistoryRepository(ApplicationDbContext context) : IUserPasswordHistoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IReadOnlyList<UserPasswordHistory>> GetRecentAsync(
        Guid userId,
        int count,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserPasswordHistories
            .Where(history => history.UserId == userId)
            .OrderByDescending(history => history.ChangedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UserPasswordHistory history, CancellationToken cancellationToken = default)
    {
        await _context.UserPasswordHistories.AddAsync(history, cancellationToken);
    }

    public async Task PruneExcessAsync(Guid userId, int maxEntries, CancellationToken cancellationToken = default)
    {
        var toRemove = await _context.UserPasswordHistories
            .Where(history => history.UserId == userId)
            .OrderByDescending(history => history.ChangedAt)
            .Skip(maxEntries)
            .ToListAsync(cancellationToken);

        if (toRemove.Count != 0)
            _context.UserPasswordHistories.RemoveRange(toRemove);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
