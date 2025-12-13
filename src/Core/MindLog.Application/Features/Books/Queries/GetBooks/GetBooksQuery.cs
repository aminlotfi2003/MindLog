using MediatR;
using MindLog.Application.Features.Books.Dtos;

namespace MindLog.Application.Features.Books.Queries.GetBooks;

public sealed record GetBooksQuery : IRequest<IReadOnlyList<BookListItemDto>>;
