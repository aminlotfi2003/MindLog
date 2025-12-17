using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Courses.Dtos;

public sealed record CourseListItemDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    bool IsDeleted,
    DateTimeOffset? DeletedAt,
    string FullName,
    string Title,
    DateOnly? CompletionDate,
    int SortOrder)
{
    public static CourseListItemDto FromEntity(TrainingCourse course) =>
        new(
            course.Id,
            course.CreatedAt,
            course.IsDeleted,
            course.DeletedAt,
            course.Profile.FullName,
            course.Title,
            course.CompletionDate,
            course.SortOrder
        );
}
