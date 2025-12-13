using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Books.Dtos;

namespace MindLog.Application.Features.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IReadOnlyList<BookListItemDto>>
{
    private readonly IBookRepository _repo;

    public GetBooksQueryHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<BookListItemDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetBooksListAsync(cancellationToken);
    }
}
