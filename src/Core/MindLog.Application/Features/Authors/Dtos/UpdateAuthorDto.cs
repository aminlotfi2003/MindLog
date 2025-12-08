using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Authors.Dtos;

public sealed record UpdateAuthorDto(
    Guid Id,
    string FirstName,
    string LastName)
{
    public static UpdateAuthorDto FromEntity(Author author) =>
        new(author.Id, author.FirstName, author.LastName);
}
