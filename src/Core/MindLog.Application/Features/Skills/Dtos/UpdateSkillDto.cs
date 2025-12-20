using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Skills.Dtos;

public record UpdateSkillDto(
    Guid Id,
    Guid ProfileId,
    string Name,
    SkillLevel Level,
    SkillCategory Category,
    int SortOrder)
{
    public static UpdateSkillDto FromEntity(SkillItem skill) =>
        new(
            skill.Id,
            skill.ProfileId,
            skill.Name,
            skill.Level,
            skill.Category,
            skill.SortOrder
        );
}
