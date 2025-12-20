using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Educations.Dtos;

namespace MindLog.Application.Features.Educations.Queries.GetDeletedEducations;

public class GetDeletedEducationsQueryHandler : IRequestHandler<GetDeletedEducationsQuery, IReadOnlyList<EducationListItemDto>>
{
    private readonly IEducationRecordRepository _repo;

    public GetDeletedEducationsQueryHandler(IEducationRecordRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<EducationListItemDto>> Handle(GetDeletedEducationsQuery request, CancellationToken cancellationToken)
    {
        var educations = await _repo.ListIncludingDeletedAsync(cancellationToken);

        var deleted = educations
            .Where(x => x.IsDeleted)
            .Select(EducationListItemDto.FromEntity)
            .ToList();

        return deleted;
    }
}
