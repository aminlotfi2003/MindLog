using MediatR;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Commands.CreateCourse;

public sealed record CreateCourseCommand(
    Guid ProfileId,
    string Title,
    string? Provider,
    string? CertificateUrl,
    DateOnly? CompletionDate,
    int SortOrder
) : IRequest<Guid>
{
    public static CreateCourseCommand FromDto(CreateCourseDto dto) =>
        new(
            dto.ProfileId,
            dto.Title,
            dto.Provider,
            dto.CertificateUrl,
            dto.CompletionDate,
            dto.SortOrder
        );
}
