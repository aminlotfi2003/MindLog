using MediatR;
using MindLog.Application.Features.Languages.Dtos;

namespace MindLog.Application.Features.Languages.Queries.GetLanguages;

public record GetLanguagesQuery : IRequest<IReadOnlyList<LanguageListItemDto>>;
