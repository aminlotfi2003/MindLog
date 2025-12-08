using MediatR;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Commands.CreateAuthor;

public sealed record CreateAuthorCommand(
    string FirstName,
    string LastName
) : IRequest<Guid>
{
    public static CreateAuthorCommand FromDto(CreateAuthorDto dto) =>
        new(dto.FirstName, dto.LastName);
}
