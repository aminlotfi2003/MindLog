using MindLog.Domain.Entities;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IAuthorRepository : IRepository<Guid, Author>
{
    Task<Author?> GetByFullNameAsync(
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByFullNameAsync(
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Author>> SearchByNameAsync(
        string searchTerm,
        CancellationToken cancellationToken = default);

    Task<Author?> GetWithBooksAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
