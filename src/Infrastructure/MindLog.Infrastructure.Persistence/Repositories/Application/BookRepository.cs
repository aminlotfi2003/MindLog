using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class BookRepository : EfRepository<Guid, Book>, IBookRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Book> _books;

    public BookRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _books = _dbContext.Set<Book>();
    }

    public async Task<Book?> GetBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be null or empty.", nameof(slug));

        var normalizedSlug = slug.Trim().ToLower();

        return await _books
            .FirstOrDefaultAsync(
                b => b.Slug.ToLower() == normalizedSlug,
                cancellationToken
            )
            .ConfigureAwait(false);
    }

    public async Task<bool> ExistsBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be null or empty.", nameof(slug));

        var normalizedSlug = slug.Trim().ToLower();

        return await _books
            .AnyAsync(
                b => b.Slug.ToLower() == normalizedSlug,
                cancellationToken
            )
            .ConfigureAwait(false);
    }
}
