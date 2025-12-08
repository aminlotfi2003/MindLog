using Microsoft.EntityFrameworkCore;
using MindLog.Domain.Entities;
using MindLog.Domain.Identity;

namespace MindLog.Application.Common.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Author> Authors { get; }
    DbSet<Book> Books { get; }

    DbSet<UserLoginHistory> UserLoginHistories { get; }
    DbSet<UserPasswordHistory> UserPasswordHistories { get; }
    DbSet<UserRefreshToken> UserRefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
