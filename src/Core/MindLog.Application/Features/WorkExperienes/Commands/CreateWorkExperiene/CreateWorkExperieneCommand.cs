using MediatR;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.WorkExperienes.Commands.CreateWorkExperiene;

public record CreateWorkExperieneCommand(
    Guid ProfileId,
    string Company,
    string RoleTitle,
    EmploymentType EmploymentType,
    WorkMode WorkMode,
    DateOnly StartDate,
    DateOnly? EndDate,
    string? Location,
    string? Description,
    int SortOrder
) : IRequest<Guid>;
