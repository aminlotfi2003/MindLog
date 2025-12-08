using MediatR;
using MindLog.Application.Features.Books.Dtos;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
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
) : IRequest<Unit>
{
    public static UpdateBookCommand FromDto(UpdateBookDto dto) =>
        new(
            dto.Id,
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
