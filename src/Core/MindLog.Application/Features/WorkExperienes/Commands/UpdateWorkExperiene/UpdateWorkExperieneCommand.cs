using MediatR;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.WorkExperienes.Commands.UpdateWorkExperiene;

public record UpdateWorkExperieneCommand(
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
    int SortOrder
) : IRequest<Unit>;
