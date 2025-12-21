using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.WorkExperienes.Dtos;

namespace MindLog.Application.Features.WorkExperienes.Queries.GetDeletedWorkExperienes;

public class GetDeletedWorkExperienesQueryHandler : IRequestHandler<GetDeletedWorkExperienesQuery, IReadOnlyList<WorkExperieneListItemDto>>
{
    private readonly IWorkExperienceRepository _repo;

    public GetDeletedWorkExperienesQueryHandler(IWorkExperienceRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<WorkExperieneListItemDto>> Handle(GetDeletedWorkExperienesQuery request, CancellationToken cancellationToken)
    {
        var experiences = await _repo.ListIncludingDeletedAsync(cancellationToken);

        var deleted = experiences
            .Where(x => x.IsDeleted)
            .Select(WorkExperieneListItemDto.FromEntity)
            .ToList();

        return deleted;
    }
}
