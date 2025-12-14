using Microsoft.EntityFrameworkCore;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Books.Dtos;
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

    public async Task<IReadOnlyList<BookListItemDto>> GetBooksListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .AsNoTracking()
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BookListItemDto(
                b.Id,
                b.Title,
                $"{b.Author.FirstName} {b.Author.LastName}",
                b.Status,
                b.Category,
                b.Rating,
                b.CreatedAt,
                b.Slug,
                b.IsDeleted,
                b.DeletedAt))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<BookListItemDto>> GetBooksListIncludingDeletedAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Books
            .IgnoreQueryFilters()
            .AsNoTracking()
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BookListItemDto(
                b.Id,
                b.Title,
                $"{b.Author.FirstName} {b.Author.LastName}",
                b.Status,
                b.Category,
                b.Rating,
                b.CreatedAt,
                b.Slug,
                b.IsDeleted,
                b.DeletedAt))
            .ToListAsync(cancellationToken);
    }

    public async Task<BookDetailsDto?> GetBookDetailsBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        var book = await _dbContext.Books
            .AsNoTracking()
            .Where(b => b.Slug == slug)
            .Select(b => new BookDetailsDto(
                b.Id,
                b.Title,
                $"{b.Author.FirstName} {b.Author.LastName}",
                b.Slug,
                b.CoverImagePath,
                b.Status,
                b.Category,
                b.ShortSummary,
                b.FullReview,
                b.Rating,
                b.CreatedAt,
                b.ModifiedAt,
                b.AuthorId,
                b.DeletedAt))
            .SingleOrDefaultAsync(cancellationToken);

        return book;
    }

    public async Task<BookDetailsDto?> GetBookDetailsAsync(
        Guid id,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Books.AsNoTracking();

        if (includeDeleted)
            query = query.IgnoreQueryFilters();

        return await query
            .Where(b => b.Id == id)
            .Select(b => new BookDetailsDto(
                b.Id,
                b.Title,
                $"{b.Author.FirstName} {b.Author.LastName}",
                b.Slug,
                b.CoverImagePath,
                b.Status,
                b.Category,
                b.ShortSummary,
                b.FullReview,
                b.Rating,
                b.CreatedAt,
                b.ModifiedAt,
                b.AuthorId,
                b.DeletedAt))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
