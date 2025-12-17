using MindLog.Domain.Common;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class TrainingCourse : EntityBase<Guid>, IAuditableEntity, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Guid ProfileId { get; set; }
    public PersonalProfile Profile { get; set; } = default!;

    public string Title { get; private set; } = default!;
    public string? Provider { get; private set; }
    public string? CertificateUrl { get; private set; }
    public DateOnly? CompletionDate { get; private set; }
    public int SortOrder { get; private set; }

    private TrainingCourse() { }

    public static TrainingCourse Create(
        Guid profileId,
        string title,
        string? provider = null,
        DateOnly? completionDate = null,
        string? certificateUrl = null,
        int sortOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Course title is required.", nameof(title));

        return new TrainingCourse
        {
            ProfileId = profileId,
            Title = title.Trim(),
            Provider = provider?.Trim(),
            CompletionDate = completionDate,
            CertificateUrl = certificateUrl?.Trim(),
            SortOrder = sortOrder
        };
    }
}
