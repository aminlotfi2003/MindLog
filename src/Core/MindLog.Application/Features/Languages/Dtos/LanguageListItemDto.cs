using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Languages.Dtos;

public record LanguageListItemDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    string FullName,
    string Language,
    LanguageLevel Level,
    int SortOrder)
{
    public static LanguageListItemDto FromEntity(LanguageProficiency language) =>
        new(
            language.Id,
            language.CreatedAt,
            language.Profile.FullName,
            language.Language,
            language.Level,
            language.SortOrder
        );
}
