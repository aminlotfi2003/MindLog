using MediatR;

namespace MindLog.Application.Features.Skills.Commands.RestoreSkill;

public record RestoreSkillCommand(Guid Id) : IRequest<Unit>;
