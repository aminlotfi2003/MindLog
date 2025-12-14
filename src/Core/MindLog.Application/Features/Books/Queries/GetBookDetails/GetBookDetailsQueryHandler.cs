using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Books.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Books.Queries.GetBookDetails;

public sealed class GetBookDetailsQueryHandler : IRequestHandler<GetBookDetailsQuery, BookDetailsDto>
{
    private readonly IBookRepository _repo;

    public GetBookDetailsQueryHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task<BookDetailsDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
    {
        var book = await _repo.GetBookDetailsAsync(
            request.Id,
            request.IncludeDeleted,
            cancellationToken
        );

        if (book is null)
            throw new NotFoundException($"Book with Id '{request.Id}' was not found.");

        return book;
    }
}
