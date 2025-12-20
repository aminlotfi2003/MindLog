using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Skills.Dtos;

namespace MindLog.Application.Features.Skills.Queries.GetDeletedSkills;

public class GetDeletedSkillsQueryHandler : IRequestHandler<GetDeletedSkillsQuery, IReadOnlyList<SkillListItemDto>
{
    private readonly ISkillItemRepository _repo;

    public GetDeletedSkillsQueryHandler(ISkillItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<SkillListItemDto>> Handle(GetDeletedSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _repo.ListIncludingDeletedAsync(cancellationToken);

        var deleted = skills
            .Where(x => x.IsDeleted)
            .Select(SkillListItemDto.FromEntity)
            .ToList();

        return deleted;
    }
}
