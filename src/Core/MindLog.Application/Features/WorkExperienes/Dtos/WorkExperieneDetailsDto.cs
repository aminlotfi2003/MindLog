using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.WorkExperienes.Dtos;

public record WorkExperieneDetailsDto(
    Guid Id,
    Guid ProfileId,
    string Company,
    string RoleTitle,
    EmploymentType EmploymentType,
    WorkMode WorkMode,
    DateOnly StartDate,
    DateOnly? EndDate,
    string? Location,
    string? Description,
    int SortOrder)
{
    public static WorkExperieneDetailsDto FromEntity(WorkExperience workExperience) =>
        new(
            workExperience.Id,
            workExperience.ProfileId,
            workExperience.Company,
            workExperience.RoleTitle,
            workExperience.EmploymentType,
            workExperience.WorkMode,
            workExperience.StartDate,
            workExperience.EndDate,
            workExperience.Location,
            workExperience.Description,
            workExperience.SortOrder
        );
}
