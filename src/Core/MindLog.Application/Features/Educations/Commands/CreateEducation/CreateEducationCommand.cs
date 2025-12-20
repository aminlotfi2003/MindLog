using MediatR;

namespace MindLog.Application.Features.Educations.Commands.CreateEducation;

public record CreateEducationCommand(
    Guid ProfileId,
    string Institution,
    string Degree,
    string FieldOfStudy,
    DateOnly? StartDate,
    DateOnly? EndDate,
    string? Description,
    int SortOrder
) : IRequest<Guid>;
