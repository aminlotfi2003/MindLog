using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Courses.Dtos;

public sealed record UpdateCourseDto(
    Guid Id,
    Guid ProfileId,
    string Title,
    string? Provider,
    string? CertificateUrl,
    DateOnly? CompletionDate,
    int SortOrder)
{
    public static UpdateCourseDto FromEntity(TrainingCourse course) =>
        new(
            course.Id,
            course.ProfileId,
            course.Title,
            course.Provider,
            course.CertificateUrl,
            course.CompletionDate,
            course.SortOrder
        );
}
