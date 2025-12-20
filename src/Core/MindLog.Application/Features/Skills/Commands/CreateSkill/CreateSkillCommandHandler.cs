using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Skills.Commands.CreateSkill;

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Guid>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly ISkillItemRepository _skillRepo;
    private readonly IUnitOfWork _uow;

    public CreateSkillCommandHandler(
        IPersonalProfileRepository profileRepo,
        ISkillItemRepository skillRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _skillRepo = skillRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
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

        var course = SkillItem.Create(
            request.ProfileId,
            request.Name,
            request.Level,
            request.Category,
            request.SortOrder
        );

        await _skillRepo.AddAsync(course, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}
