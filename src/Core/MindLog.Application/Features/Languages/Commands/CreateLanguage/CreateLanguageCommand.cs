using MediatR;
using MindLog.Domain.Enums;

namespace MindLog.Application.Features.Languages.Commands.CreateLanguage;

public record CreateLanguageCommand(
    Guid ProfileId,
    string Language,
    LanguageLevel Level,
    string? Certificate,
    int SortOrder
) : IRequest<Guid>;
