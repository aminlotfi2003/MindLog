using MediatR;
using MindLog.Application.Features.Books.Dtos;

namespace MindLog.Application.Features.Books.Queries.GetDeletedBooks;

public sealed record GetDeletedBooksQuery : IRequest<IReadOnlyList<BookListItemDto>>;
