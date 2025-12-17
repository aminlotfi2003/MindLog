using MindLog.Domain.Common;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class EducationRecord : EntityBase<Guid>, IAuditableEntity, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Guid ProfileId { get; set; }
    public PersonalProfile Profile { get; set; } = default!;

    public string Institution { get; private set; } = default!;
    public string Degree { get; private set; } = default!;
    public string FieldOfStudy { get; private set; } = default!;
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public string? Description { get; private set; }
    public int SortOrder { get; private set; }

    private EducationRecord() { }

    public static EducationRecord Create(
        Guid profileId,
        string institution,
        string degree,
        string fieldOfStudy,
        DateOnly? startDate = null,
        DateOnly? endDate = null,
        string? description = null,
        int sortOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(institution))
            throw new ArgumentException("Institution is required.", nameof(institution));
        if (string.IsNullOrWhiteSpace(degree))
            throw new ArgumentException("Degree is required.", nameof(degree));
        if (string.IsNullOrWhiteSpace(fieldOfStudy))
            throw new ArgumentException("Field of study is required.", nameof(fieldOfStudy));

        if (startDate.HasValue && endDate.HasValue && endDate.Value < startDate.Value)
            throw new ArgumentException("EndDate cannot be earlier than StartDate.", nameof(endDate));

        return new EducationRecord
        {
            ProfileId = profileId,
            Institution = institution.Trim(),
            Degree = degree.Trim(),
            FieldOfStudy = fieldOfStudy.Trim(),
            StartDate = startDate,
            EndDate = endDate,
            Description = description?.Trim(),
            SortOrder = sortOrder
        };
    }
}
