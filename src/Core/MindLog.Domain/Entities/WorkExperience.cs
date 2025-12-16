using MindLog.Domain.Common;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class WorkExperience : EntityBase<Guid>, IAuditableEntity, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Guid ProfileId { get; set; }
    public PersonalProfile Profile { get; set; } = default!;

    public string Company { get; private set; } = default!;
    public string RoleTitle { get; private set; } = default!;
    public EmploymentType EmploymentType { get; private set; }
    public WorkMode WorkMode { get; private set; }

    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; } // null => current
    public string? Location { get; private set; }
    public string? Description { get; private set; }

    public int SortOrder { get; private set; }

    private WorkExperience() { }

    public static WorkExperience Create(
        Guid profileId,
        string company,
        string roleTitle,
        DateOnly startDate,
        DateOnly? endDate,
        EmploymentType employmentType = EmploymentType.FullTime,
        WorkMode workMode = WorkMode.OnSite,
        string? location = null,
        string? description = null,
        int sortOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(company))
            throw new ArgumentException("Company is required.", nameof(company));
        if (string.IsNullOrWhiteSpace(roleTitle))
            throw new ArgumentException("Role title is required.", nameof(roleTitle));
        if (endDate.HasValue && endDate.Value < startDate)
            throw new ArgumentException("EndDate cannot be earlier than StartDate.", nameof(endDate));

        return new WorkExperience
        {
            ProfileId = profileId,
            Company = company.Trim(),
            RoleTitle = roleTitle.Trim(),
            StartDate = startDate,
            EndDate = endDate,
            EmploymentType = employmentType,
            WorkMode = workMode,
            Location = location?.Trim(),
            Description = description?.Trim(),
            SortOrder = sortOrder
        };
    }

    public bool IsCurrent => EndDate is null;
}
