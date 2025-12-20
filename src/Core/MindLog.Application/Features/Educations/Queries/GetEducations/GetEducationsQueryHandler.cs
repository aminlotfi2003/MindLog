using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Educations.Dtos;

namespace MindLog.Application.Features.Educations.Queries.GetEducations;

public class GetEducationsQueryHandler : IRequestHandler<GetEducationsQuery, IReadOnlyList<EducationListItemDto>>
{
    private readonly IEducationRecordRepository _repo;

    public GetEducationsQueryHandler(IEducationRecordRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<EducationListItemDto>> Handle(GetEducationsQuery request, CancellationToken cancellationToken)
    {
        var education = await _repo.ListAsync(cancellationToken);

        return education.Select(EducationListItemDto.FromEntity).ToList();
    }
}
