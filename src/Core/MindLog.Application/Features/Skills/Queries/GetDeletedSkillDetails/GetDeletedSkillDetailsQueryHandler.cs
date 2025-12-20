using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Skills.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Skills.Queries.GetDeletedSkillDetails;

public class GetDeletedSkillDetailsQueryHandler : IRequestHandler<GetDeletedSkillDetailsQuery, SkillDetailsDto>
{
    private readonly ISkillItemRepository _repo;

    public GetDeletedSkillDetailsQueryHandler(ISkillItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<SkillDetailsDto> Handle(GetDeletedSkillDetailsQuery request, CancellationToken cancellationToken)
    {
        var skill = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (skill is null)
            throw new NotFoundException($"Skill with ID '{request.Id}' was not found.");

        return SkillDetailsDto.FromEntity(skill);
    }
}
