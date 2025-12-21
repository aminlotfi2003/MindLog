using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.WorkExperienes.Dtos;

namespace MindLog.Application.Features.WorkExperienes.Queries.GetWorkExperienes;

public class GetWorkExperienesQueryHandler : IRequestHandler<GetWorkExperienesQuery, IReadOnlyList<WorkExperieneListItemDto>>
{
    private readonly IWorkExperienceRepository _repo;

    public GetWorkExperienesQueryHandler(IWorkExperienceRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<WorkExperieneListItemDto>> Handle(GetWorkExperienesQuery request, CancellationToken cancellationToken)
    {
        var experiences = await _repo.ListAsync(cancellationToken);

        return experiences.Select(WorkExperieneListItemDto.FromEntity).ToList();
    }
}
