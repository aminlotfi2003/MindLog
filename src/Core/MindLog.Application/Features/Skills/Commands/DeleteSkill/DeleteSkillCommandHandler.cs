using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Skills.Commands.DeleteSkill;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Unit>
{
    private readonly ISkillItemRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteSkillCommandHandler(ISkillItemRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (skill is null)
            throw new NotFoundException($"Skill with ID '{request.Id}' was not found.");

        if (skill.IsDeleted)
            return Unit.Value;

        await _repo.Update(skill, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
