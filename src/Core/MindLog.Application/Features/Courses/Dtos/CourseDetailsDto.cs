using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Courses.Dtos;

public sealed record CourseDetailsDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ModifiedAt,
    DateTimeOffset? DeletedAt,
    Guid ProfileId,
    string Title,
    string? Provider,
    string? CertificateUrl,
    DateOnly? CompletionDate,
    int SortOrder)
{
    public static CourseDetailsDto FromEntity(TrainingCourse course) =>
        new(
            course.Id,
            course.CreatedAt,
            course.ModifiedAt,
            course.DeletedAt,
            course.ProfileId,
            course.Title,
            course.Provider,
            course.CertificateUrl,
            course.CompletionDate,
            course.SortOrder
        );
}
