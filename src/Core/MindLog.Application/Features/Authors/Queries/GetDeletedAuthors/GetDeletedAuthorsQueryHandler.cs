using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Queries.GetDeletedAuthors;

public sealed class GetDeletedAuthorsQueryHandler
    : IRequestHandler<GetDeletedAuthorsQuery, IReadOnlyList<AuthorListItemDto>>
{
    private readonly IAuthorRepository _repo;

    public GetDeletedAuthorsQueryHandler(IAuthorRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<AuthorListItemDto>> Handle(
        GetDeletedAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await _repo.ListIncludingDeletedAsync(cancellationToken);

        var deleted = authors
            .Where(a => a.IsDeleted)
            .Select(AuthorListItemDto.FromEntity)
            .ToList();

        return deleted;
    }
}
