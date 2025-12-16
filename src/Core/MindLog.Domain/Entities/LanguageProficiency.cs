using MindLog.Domain.Common;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class LanguageProficiency : EntityBase<Guid>, IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }

    public Guid ProfileId { get; set; }
    public PersonalProfile Profile { get; set; } = default!;

    public string Language { get; private set; } = default!;
    public LanguageLevel Level { get; private set; }
    public string? Certificate { get; private set; }
    public int SortOrder { get; private set; }

    private LanguageProficiency() { }

    public static LanguageProficiency Create(
        Guid profileId,
        string language,
        LanguageLevel level,
        string? certificate = null,
        int sortOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(language))
            throw new ArgumentException("Language is required.", nameof(language));

        return new LanguageProficiency
        {
            ProfileId = profileId,
            Language = language.Trim(),
            Level = level,
            Certificate = certificate?.Trim(),
            SortOrder = sortOrder
        };
    }
}
