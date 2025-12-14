using MindLog.Application.Features.Books.Dtos;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IBookRepository : IRepository<Guid, Book>
{
    Task<Book?> GetBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);

    Task<BookDetailsDto?> GetBookDetailsBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BookListItemDto>> GetBooksListAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BookListItemDto>> GetBooksListIncludingDeletedAsync(CancellationToken cancellationToken = default);

    Task<BookDetailsDto?> GetBookDetailsAsync(
        Guid id,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);
}
