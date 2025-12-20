using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Skills.Dtos;

public record SkillListItemDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    bool IsDeleted,
    DateTimeOffset? DeletedAt,
    string FullName,
    string Name,
    SkillLevel Level,
    int SortOrder)
{
    public static SkillListItemDto FromEntity(SkillItem skill) =>
        new(
            skill.Id,
            skill.CreatedAt,
            skill.IsDeleted,
            skill.DeletedAt,
            skill.Profile.FullName,
            skill.Name,
            skill.Level,
            skill.SortOrder
        );
}
