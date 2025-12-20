using MediatR;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Languages.Commands.UpdateLanguage;

public record UpdateLanguageCommand(
    Guid Id,
    Guid ProfileId,
    string Language,
    LanguageLevel Level,
    string? Certificate,
    int SortOrder
) : IRequest<Unit>;
