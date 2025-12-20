using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Languages.Dtos;

namespace MindLog.Application.Features.Languages.Queries.GetLanguages;

public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, IReadOnlyList<LanguageListItemDto>>
{
    private readonly ILanguageProficiencyRepository _repo;

    public GetLanguagesQueryHandler(ILanguageProficiencyRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<LanguageListItemDto>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _repo.ListAsync(cancellationToken);

        return languages.Select(LanguageListItemDto.FromEntity).ToList();
    }
}
