using MediatR;

namespace MindLog.Application.Features.WorkExperienes.Commands.DeleteWorkExperiene;

public record DeleteWorkExperieneCommand(Guid Id) : IRequest<Unit>;
