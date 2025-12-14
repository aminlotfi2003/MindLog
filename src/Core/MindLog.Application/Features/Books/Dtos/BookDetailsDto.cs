using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Books.Dtos;

public sealed record BookDetailsDto(
    Guid Id,
    string Title,
    string AuthorName,
    string Slug,
    string CoverImagePath,
    ReadingStatus Status,
    BookCategory Category,
    string ShortSummary,
    string FullReview,
    int? Rating,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ModifiedAt,
    Guid AuthorId,
    DateTimeOffset? DeletedAt
)
{
    public static BookDetailsDto FromEntity(Book book) =>
        new(
            book.Id,
            book.Title,
            $"{book.Author.FirstName} {book.Author.LastName}",
            book.Slug,
            book.CoverImagePath,
            book.Status,
            book.Category,
            book.ShortSummary,
            book.FullReview,
            book.Rating,
            book.CreatedAt,
            book.ModifiedAt,
            book.AuthorId,
            book.DeletedAt
        );
}
