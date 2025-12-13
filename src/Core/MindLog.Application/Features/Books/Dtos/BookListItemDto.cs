using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Books.Dtos;

public sealed record BookListItemDto(
    Guid Id,
    string Title,
    string AuthorName,
    BookCategory Category,
    int? Rating,
    DateTimeOffset CreatedAt
)
{
    public static BookListItemDto FromEntity(Book book) =>
        new(
            book.Id,
            book.Title,
            $"{book.Author.FirstName} {book.Author.LastName}",
            book.Category,
            book.Rating,
            book.CreatedAt
        );
}
