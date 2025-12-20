using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Skills.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Skills.Queries.GetSkillDetails;

public class GetSkillDetailsQueryHandler : IRequestHandler<GetSkillDetailsQuery, SkillDetailsDto>
{
    private readonly ISkillItemRepository _repo;

    public GetSkillDetailsQueryHandler(ISkillItemRepository repo)
    {
        _repo = repo;
    }

    public async Task<SkillDetailsDto> Handle(GetSkillDetailsQuery request, CancellationToken cancellationToken)
    {
        var skill = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (skill is null)
            throw new NotFoundException($"Skill with ID '{request.Id}' was not found.");

        return SkillDetailsDto.FromEntity(skill);
    }
}
