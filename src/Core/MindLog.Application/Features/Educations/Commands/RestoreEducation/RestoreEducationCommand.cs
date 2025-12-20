using MediatR;

namespace MindLog.Application.Features.Educations.Commands.RestoreEducation;

public record RestoreEducationCommand(Guid Id) : IRequest<Unit>;
