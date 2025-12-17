using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Courses.Dtos;

public sealed record CreateCourseDto(
    Guid ProfileId,
    string Title,
    string? Provider,
    string? CertificateUrl,
    DateOnly? CompletionDate,
    int SortOrder)
{
    public static CreateCourseDto FromEntity(TrainingCourse course) =>
        new(
            course.ProfileId,
            course.Title,
            course.Provider,
            course.CertificateUrl,
            course.CompletionDate,
            course.SortOrder
        );
}
