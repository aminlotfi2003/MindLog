using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Educations.Commands.RestoreEducation;

public class RestoreEducationCommandHandler : IRequestHandler<RestoreEducationCommand, Unit>
{
    private readonly IEducationRecordRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreEducationCommandHandler(IEducationRecordRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(RestoreEducationCommand request, CancellationToken cancellationToken)
    {
        var education = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (education is null)
            throw new NotFoundException($"Education with ID '{request.Id}' was not found.");

        if (!education.IsDeleted)
            return Unit.Value;

        var existsDuplicateActive = await _repo.AnyAsync(
            b => !b.IsDeleted &&
                  b.Id != education.Id &&
                  b.FieldOfStudy == education.FieldOfStudy &&
                  b.ProfileId == education.ProfileId,
            cancellationToken
        );

        if (existsDuplicateActive)
            throw new ConflictException("Another active education with the same field already exists.");

        await _repo.Update(education, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
