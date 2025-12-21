using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.WorkExperienes.Commands.DeleteWorkExperiene;

public class DeleteWorkExperieneCommandHandler : IRequestHandler<DeleteWorkExperieneCommand, Unit>
{
    private readonly IWorkExperienceRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteWorkExperieneCommandHandler(IWorkExperienceRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteWorkExperieneCommand request, CancellationToken cancellationToken)
    {
        var experience = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (experience is null)
            throw new NotFoundException($"Work experience with ID '{request.Id}' was not found.");

        if (experience.IsDeleted)
            return Unit.Value;

        await _repo.Update(experience, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
