using MediatR;

namespace MindLog.Application.Features.Skills.Commands.DeleteSkill;

public record DeleteSkillCommand(Guid Id) : IRequest<Unit>;
