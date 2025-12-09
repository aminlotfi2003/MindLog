using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Authors.Dtos;

public sealed record AuthorDetailsDto(
    Guid Id,
    string FirstName,
    string LastName,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ModifiedAt,
    IReadOnlyCollection<AuthorBookSummaryDto> Books
)
{
    public static AuthorDetailsDto FromEntity(Author author) =>
        new(
            author.Id,
            author.FirstName,
            author.LastName,
            author.CreatedAt,
            author.ModifiedAt,
            author.Books
                .Select(b => new AuthorBookSummaryDto(
                    b.Id,
                    b.Title,
                    b.Status,
                    b.Category,
                    b.Rating))
                .ToArray()
        );
}
