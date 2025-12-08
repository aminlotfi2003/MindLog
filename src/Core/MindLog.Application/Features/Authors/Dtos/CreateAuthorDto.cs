using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Authors.Dtos;

public sealed record CreateAuthorDto(
    string FirstName,
    string LastName)
{
    public static CreateAuthorDto FromEntity(Author author) =>
        new(author.FirstName,
            author.LastName);
}
