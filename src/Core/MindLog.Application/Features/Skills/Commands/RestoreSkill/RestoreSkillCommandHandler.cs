using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Skills.Commands.RestoreSkill;

public class RestoreSkillCommandHandler : IRequestHandler<RestoreSkillCommand, Unit>
{
    private readonly ISkillItemRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreSkillCommandHandler(ISkillItemRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(RestoreSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (skill is null)
            throw new NotFoundException($"Skill with ID '{request.Id}' was not found.");

        if (!skill.IsDeleted)
            return Unit.Value;

        var existsDuplicateActive = await _repo.AnyAsync(
            b => !b.IsDeleted &&
                  b.Id != skill.Id &&
                  b.Name == skill.Name &&
                  b.ProfileId == skill.ProfileId,
            cancellationToken
        );

        if (existsDuplicateActive)
            throw new ConflictException("Another active skill with the same title already exists.");

        await _repo.Update(skill, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
