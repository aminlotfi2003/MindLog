using MediatR;

namespace MindLog.Application.Features.Educations.Commands.UpdateEducation;

public record UpdateEducationCommand(
    Guid Id,
    Guid ProfileId,
    string Institution,
    string Degree,
    string FieldOfStudy,
    DateOnly? StartDate,
    DateOnly? EndDate,
    string? Description,
    int SortOrder
) : IRequest<Unit>;
