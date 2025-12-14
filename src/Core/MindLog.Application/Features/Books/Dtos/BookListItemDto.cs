using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Books.Dtos;

public sealed record BookListItemDto(
    Guid Id,
    string Title,
    string AuthorName,
    ReadingStatus Status,
    BookCategory Category,
    int? Rating,
    DateTimeOffset CreatedAt,
    string Slug,
    bool IsDeleted,
    DateTimeOffset? DeletedAt
)
{
    public static BookListItemDto FromEntity(Book book) =>
        new(
            book.Id,
            book.Title,
            $"{book.Author.FirstName} {book.Author.LastName}",
            book.Status,
            book.Category,
            book.Rating,
            book.CreatedAt,
            book.Slug,
            book.IsDeleted,
            book.DeletedAt
        );
}
