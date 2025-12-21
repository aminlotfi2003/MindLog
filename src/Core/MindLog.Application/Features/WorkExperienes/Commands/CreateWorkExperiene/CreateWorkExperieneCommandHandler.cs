using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.WorkExperienes.Commands.CreateWorkExperiene;

public class CreateWorkExperieneCommandHandler : IRequestHandler<CreateWorkExperieneCommand, Guid>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly IWorkExperienceRepository _experienceRepo;
    private readonly IUnitOfWork _uow;

    public CreateWorkExperieneCommandHandler(
        IPersonalProfileRepository profileRepo,
        IWorkExperienceRepository experienceRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _experienceRepo = experienceRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateWorkExperieneCommand request, CancellationToken cancellationToken)
    {
        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with Id '{request.ProfileId}' was not found.");

        var experience = WorkExperience.Create(
            request.ProfileId,
            request.Company,
            request.RoleTitle,
            request.StartDate,
            request.EndDate,
            request.EmploymentType,
            request.WorkMode,
            request.Location,
            request.Description,
            request.SortOrder
        );

        await _experienceRepo.AddAsync(experience, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return experience.Id;
    }
}
