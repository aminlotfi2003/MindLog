using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Queries.GetAuthors;

public sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IReadOnlyList<AuthorListItemDto>>
{
    private readonly IAuthorRepository _repo;

    public GetAuthorsQueryHandler(IAuthorRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<AuthorListItemDto>> Handle(
        GetAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        var authors = await _repo.ListAsync(cancellationToken);

        return authors
            .Select(AuthorListItemDto.FromEntity)
            .ToList();
    }
}
