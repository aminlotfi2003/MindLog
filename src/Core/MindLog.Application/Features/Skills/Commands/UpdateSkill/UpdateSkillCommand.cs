using MediatR;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Skills.Commands.UpdateSkill;

public record UpdateSkillCommand(
    Guid Id,
    Guid ProfileId,
    string Name,
    SkillLevel Level,
    SkillCategory Category,
    int SortOrder
) : IRequest<Unit>;
