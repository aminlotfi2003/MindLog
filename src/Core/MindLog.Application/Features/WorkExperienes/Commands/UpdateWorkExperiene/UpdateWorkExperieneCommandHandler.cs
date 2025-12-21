using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.WorkExperienes.Commands.UpdateWorkExperiene;

public class UpdateWorkExperieneCommandHandler : IRequestHandler<UpdateWorkExperieneCommand, Unit>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly IWorkExperienceRepository _experienceRepo;
    private readonly IUnitOfWork _uow;

    public UpdateWorkExperieneCommandHandler(
        IPersonalProfileRepository profileRepo,
        IWorkExperienceRepository experienceRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _experienceRepo = experienceRepo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateWorkExperieneCommand request, CancellationToken cancellationToken)
    {
        var experience = await _experienceRepo.GetByIdAsync(request.Id, cancellationToken);
        if (experience is null)
            throw new NotFoundException($"Work experience with ID '{request.Id}' was not found.");

        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with Id '{request.ProfileId}' was not found.");

        await _experienceRepo.Update(experience, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
