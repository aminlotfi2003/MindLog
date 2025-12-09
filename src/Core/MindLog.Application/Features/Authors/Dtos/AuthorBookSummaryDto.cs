using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Authors.Dtos;

public sealed record AuthorBookSummaryDto(
    Guid Id,
    string Title,
    ReadingStatus Status,
    BookCategory Category,
    int? Rating
);
