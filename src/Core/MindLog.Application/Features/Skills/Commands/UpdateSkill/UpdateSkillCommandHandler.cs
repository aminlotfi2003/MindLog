using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Skills.Commands.UpdateSkill;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, Unit>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly ISkillItemRepository _skillRepo;
    private readonly IUnitOfWork _uow;

    public UpdateSkillCommandHandler(
        IPersonalProfileRepository profileRepo,
        ISkillItemRepository skillRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _skillRepo = skillRepo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepo.GetByIdAsync(request.Id, cancellationToken);
        if (skill is null)
            throw new NotFoundException($"Skill with ID '{request.Id}' was not found.");

        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with Id '{request.ProfileId}' was not found.");

        var titleExists = await _skillRepo.AnyAsync(
            x => x.Name == request.Name,
            cancellationToken
        );
        if (titleExists)
            throw new ConflictException($"A skill with name '{request.Name}' already exists.");

        await _skillRepo.Update(skill, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
