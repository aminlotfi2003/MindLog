using MindLog.Domain.Entities;

namespace MindLog.Application.Features.Educations.Dtos;

public record CreateEducationDto(
    Guid ProfileId,
    string Institution,
    string Degree,
    string FieldOfStudy,
    DateOnly? StartDate,
    DateOnly? EndDate,
    string? Description,
    int SortOrder)
{
    public static CreateEducationDto FromEntity(EducationRecord education) =>
        new(
            education.ProfileId,
            education.Institution,
            education.Degree,
            education.FieldOfStudy,
            education.StartDate,
            education.EndDate,
            education.Description,
            education.SortOrder
        );
}
