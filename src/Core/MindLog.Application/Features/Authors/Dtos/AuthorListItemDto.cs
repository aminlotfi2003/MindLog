using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Authors.Dtos;

public sealed record AuthorListItemDto(
    Guid Id,
    string FirstName,
    string LastName,
    int BooksCount
)
{
    public static AuthorListItemDto FromEntity(Author author) =>
        new(
            author.Id,
            author.FirstName,
            author.LastName,
            author.Books?.Count ?? 0
        );
}
