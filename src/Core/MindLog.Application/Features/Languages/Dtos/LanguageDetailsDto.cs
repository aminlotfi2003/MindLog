using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Languages.Dtos;

public record LanguageDetailsDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ModifiedAt,
    Guid ProfileId,
    string Language,
    LanguageLevel Level,
    string? Certificate,
    int SortOrder)
{
    public static LanguageDetailsDto FromEntity(LanguageProficiency language) =>
        new(
            language.Id,
            language.CreatedAt,
            language.ModifiedAt,
            language.ProfileId,
            language.Language,
            language.Level,
            language.Certificate,
            language.SortOrder
        );
}
