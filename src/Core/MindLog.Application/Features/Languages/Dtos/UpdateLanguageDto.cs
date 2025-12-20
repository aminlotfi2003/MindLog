using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Languages.Dtos;

public record UpdateLanguageDto(
    Guid Id,
    Guid ProfileId,
    string Language,
    LanguageLevel Level,
    string? Certificate,
    int SortOrder)
{
    public static UpdateLanguageDto FromEntity(LanguageProficiency language) =>
        new(
            language.Id,
            language.ProfileId,
            language.Language,
            language.Level,
            language.Certificate,
            language.SortOrder
        );
}
