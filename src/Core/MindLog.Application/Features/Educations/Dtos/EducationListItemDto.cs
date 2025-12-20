using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Educations.Dtos;

public record EducationListItemDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    bool IsDeleted,
    DateTimeOffset? DeletedAt,
    string FullName,
    string Degree,
    string FieldOfStudy)
{
    public static EducationListItemDto FromEntity(EducationRecord education) =>
        new(
            education.Id,
            education.CreatedAt,
            education.IsDeleted,
            education.DeletedAt,
            education.Profile.FullName,
            education.Degree,
            education.FieldOfStudy
        );
}
