using MindLog.Domain.Entities;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IAuthorRepository : IRepository<Guid, Author>
{
    Task<Author?> GetWithBooksAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Author?> GetWithBooksIncludingDeletedAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
