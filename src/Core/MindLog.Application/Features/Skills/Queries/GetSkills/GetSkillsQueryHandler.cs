using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Skills.Dtos;

namespace MindLog.Application.Features.Skills.Queries.GetSkills;

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, IReadOnlyList<SkillListItemDto>>
{
    private readonly ISkillItemRepository _repo;

    public GetSkillsQueryHandler(ISkillItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<SkillListItemDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _repo.ListAsync(cancellationToken);

        return skills.Select(SkillListItemDto.FromEntity).ToList();
    }
}
