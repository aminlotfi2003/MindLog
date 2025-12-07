using MindLog.Domain.Entities;
using MindLog.Domain.Enums;
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

    Task<IReadOnlyList<Book>> ListByAuthorIdAsync(
        Guid authorId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Book>> ListByStatusAsync(
        ReadingStatus status,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Book>> ListByCategoryAsync(
        BookCategory category,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Book>> SearchByTitleAsync(
        string searchTerm,
        CancellationToken cancellationToken = default);
}
