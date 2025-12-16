using MindLog.Domain.Common;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class SkillItem : EntityBase<Guid>, IAuditableEntity, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Guid ProfileId { get; set; }
    public PersonalProfile Profile { get; set; } = default!;

    public string Name { get; private set; } = default!;
    public SkillLevel Level { get; private set; }
    public SkillCategory Category { get; private set; }
    public int SortOrder { get; private set; }

    private SkillItem() { }

    public static SkillItem Create(
        Guid profileId,
        string name,
        SkillLevel level = SkillLevel.Intermediate,
        SkillCategory category = SkillCategory.Technical,
        int sortOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Skill name is required.", nameof(name));

        return new SkillItem
        {
            ProfileId = profileId,
            Name = name.Trim(),
            Level = level,
            Category = category,
            SortOrder = sortOrder
        };
    }
}

