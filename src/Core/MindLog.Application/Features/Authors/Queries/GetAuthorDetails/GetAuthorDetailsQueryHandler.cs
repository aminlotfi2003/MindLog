using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Authors.Queries.GetAuthorDetails;

public sealed class GetAuthorDetailsQueryHandler : IRequestHandler<GetAuthorDetailsQuery, AuthorDetailsDto>
{
    private readonly IAuthorRepository _repo;

    public GetAuthorDetailsQueryHandler(IAuthorRepository repo)
    {
        _repo = repo;
    }

    public async Task<AuthorDetailsDto> Handle(
        GetAuthorDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var author = await _repo.GetWithBooksAsync(request.Id, cancellationToken);
        if (author is null)
            throw new NotFoundException($"Author with Id '{request.Id}' was not found.");

        return AuthorDetailsDto.FromEntity(author);
    }
}
