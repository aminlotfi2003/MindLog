using MediatR;

namespace MindLog.Application.Features.Educations.Commands.DeleteEducation;

public record DeleteEducationCommand(Guid Id) : IRequest<Unit>;
