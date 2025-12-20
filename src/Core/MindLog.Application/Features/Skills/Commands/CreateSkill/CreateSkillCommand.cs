using MediatR;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Skills.Commands.CreateSkill;

public record CreateSkillCommand(
    Guid ProfileId,
    string Name,
    SkillLevel Level,
    SkillCategory Category,
    int SortOrder
) : IRequest<Guid>;
