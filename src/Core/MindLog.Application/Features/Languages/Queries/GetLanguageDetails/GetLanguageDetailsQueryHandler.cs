using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Languages.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Languages.Queries.GetLanguageDetails;

public class GetLanguageDetailsQueryHandler : IRequestHandler<GetLanguageDetailsQuery, LanguageDetailsDto>
{
    private readonly ILanguageProficiencyRepository _repo;

    public GetLanguageDetailsQueryHandler(ILanguageProficiencyRepository repo)
    {
        _repo = repo;
    }

    public async Task<LanguageDetailsDto> Handle(GetLanguageDetailsQuery request, CancellationToken cancellationToken)
    {
        var language = await _repo.GetByIdAsync(
            request.Id,
            cancellationToken
        );

        if (language is null)
            throw new NotFoundException($"Language with Id '{request.Id}' was not found.");

        return LanguageDetailsDto.FromEntity(language);
    }
}
