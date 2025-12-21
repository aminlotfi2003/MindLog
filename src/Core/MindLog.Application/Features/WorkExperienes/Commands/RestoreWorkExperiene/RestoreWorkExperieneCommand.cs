using MediatR;

namespace MindLog.Application.Features.WorkExperienes.Commands.RestoreWorkExperiene;

public record RestoreWorkExperieneCommand(Guid Id) : IRequest<Unit>;
