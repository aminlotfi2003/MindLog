using MindLog.Domain.Entities;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Languages.Dtos;

public record CreateLanguageDto(
    Guid ProfileId,
    string Language,
    LanguageLevel Level,
    string? Certificate,
    int SortOrder)
{
    public static CreateLanguageDto FromEntity(LanguageProficiency language) =>
        new(
            language.ProfileId,
            language.Language,
            language.Level,
            language.Certificate,
            language.SortOrder
        );
}
