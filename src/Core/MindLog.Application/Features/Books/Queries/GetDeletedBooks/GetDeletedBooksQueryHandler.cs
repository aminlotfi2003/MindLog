using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Books.Dtos;

namespace MindLog.Application.Features.Books.Queries.GetDeletedBooks;

public sealed class GetDeletedBooksQueryHandler
    : IRequestHandler<GetDeletedBooksQuery, IReadOnlyList<BookListItemDto>>
{
    private readonly IBookRepository _repo;

    public GetDeletedBooksQueryHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<BookListItemDto>> Handle(
        GetDeletedBooksQuery request,
        CancellationToken cancellationToken)
    {
        return await _repo.GetBooksListIncludingDeletedAsync(cancellationToken);
    }
}
