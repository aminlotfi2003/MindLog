using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Books.Dtos;

public sealed record UpdateBookDto(
    Guid Id,
    Guid AuthorId,
    string Title,
    string Slug,
    string CoverImagePath,
    ReadingStatus Status,
    BookCategory Category,
    string ShortSummary,
    string FullReview,
    int? Rating
)
{
    public static UpdateBookDto FromEntity(Book book) =>
        new(
            book.Id,
            book.AuthorId,
            book.Title,
            book.Slug,
            book.CoverImagePath,
            book.Status,
            book.Category,
            book.ShortSummary,
            book.FullReview,
            book.Rating
        );
}
