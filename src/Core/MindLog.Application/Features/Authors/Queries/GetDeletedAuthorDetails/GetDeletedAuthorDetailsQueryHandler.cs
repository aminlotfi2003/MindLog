using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Authors.Queries.GetDeletedAuthorDetails;

public class GetDeletedAuthorDetailsQueryHandler : IRequestHandler<GetDeletedAuthorDetailsQuery, AuthorDetailsDto>
{
    private readonly IAuthorRepository _repo;

    public GetDeletedAuthorDetailsQueryHandler(IAuthorRepository repo)
    {
        _repo = repo;
    }

    public async Task<AuthorDetailsDto> Handle(
        GetDeletedAuthorDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var author = await _repo.GetWithBooksIncludingDeletedAsync(request.Id, cancellationToken);
        if (author is null)
            throw new NotFoundException($"Author with Id '{request.Id}' was not found.");

        return AuthorDetailsDto.FromEntity(author);
    }
}
