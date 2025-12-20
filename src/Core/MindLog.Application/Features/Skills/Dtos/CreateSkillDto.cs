using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Skills.Dtos;

public record CreateSkillDto(
    Guid ProfileId,
    string Name,
    SkillLevel Level,
    SkillCategory Category,
    int SortOrder)
{
    public static CreateSkillDto FromEntity(SkillItem skill) =>
        new(
            skill.ProfileId,
            skill.Name,
            skill.Level,
            skill.Category,
            skill.SortOrder
        );
}
