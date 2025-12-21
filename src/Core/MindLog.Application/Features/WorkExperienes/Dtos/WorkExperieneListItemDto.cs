using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.WorkExperienes.Dtos;

public record WorkExperieneListItemDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    bool IsDeleted,
    DateTimeOffset? DeletedAt,
    string FullName,
    string Company,
    string RoleTitle,
    EmploymentType EmploymentType,
    WorkMode WorkMode,
    int SortOrder)
{
    public static WorkExperieneListItemDto FromEntity(WorkExperience workExperience) =>
        new(
            workExperience.Id,
            workExperience.CreatedAt,
            workExperience.IsDeleted,
            workExperience.DeletedAt,
            workExperience.Profile.FullName,
            workExperience.Company,
            workExperience.RoleTitle,
            workExperience.EmploymentType,
            workExperience.WorkMode,
            workExperience.SortOrder
        );
}
