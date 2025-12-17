using MediatR;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Commands.UpdateCourse;

public sealed record UpdateCourseCommand(
    Guid Id,
    Guid ProfileId,
    string Title,
    string? Provider,
    string? CertificateUrl,
    DateOnly? CompletionDate,
    int SortOrder
) : IRequest<Unit>
{
    public static UpdateCourseCommand FromDto(UpdateCourseDto dto) =>
        new(
            dto.Id,
            dto.ProfileId,
            dto.Title,
            dto.Provider,
            dto.CertificateUrl,
            dto.CompletionDate,
            dto.SortOrder
        );
}
