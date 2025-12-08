using MediatR;
using MindLog.Application.Features.Books.Dtos;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Books.Commands.CreateBook;

public sealed record CreateBookCommand(
    Guid AuthorId,
    string Title,
    string Slug,
    string CoverImagePath,
    ReadingStatus Status,
    BookCategory Category,
    string ShortSummary,
    string FullReview,
    int? Rating
) : IRequest<Guid>
{
    public static CreateBookCommand FromDto(CreateBookDto dto) =>
        new(
            dto.AuthorId,
            dto.Title,
            dto.Slug,
            dto.CoverImagePath,
            dto.Status,
            dto.Category,
            dto.ShortSummary,
            dto.FullReview,
            dto.Rating
        );
}
