using MediatR;
using MindLog.Application.Features.Authors.Dtos;

namespace MindLog.Application.Features.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorCommand(
    Guid Id,
    string FirstName,
    string LastName
) : IRequest<Unit>
{
    public static UpdateAuthorCommand FromDto(UpdateAuthorDto dto) =>
        new(dto.Id, dto.FirstName, dto.LastName);
}
