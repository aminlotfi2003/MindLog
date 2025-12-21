using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.WorkExperienes.Commands.RestoreWorkExperiene;

public class RestoreWorkExperieneCommandHandler : IRequestHandler<RestoreWorkExperieneCommand, Unit>
{
    private readonly IWorkExperienceRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreWorkExperieneCommandHandler(IWorkExperienceRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(RestoreWorkExperieneCommand request, CancellationToken cancellationToken)
    {
        var experience = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (experience is null)
            throw new NotFoundException($"Work experience with ID '{request.Id}' was not found.");

        if (!experience.IsDeleted)
            return Unit.Value;

        var existsDuplicateActive = await _repo.AnyAsync(
            b => !b.IsDeleted &&
                  b.Id != experience.Id &&
                  b.Company == experience.Company &&
                  b.RoleTitle == experience.RoleTitle &&
                  b.ProfileId == experience.ProfileId,
            cancellationToken
        );

        if (existsDuplicateActive)
            throw new ConflictException("Another active work experience with the same title already exists.");

        await _repo.Update(experience, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
