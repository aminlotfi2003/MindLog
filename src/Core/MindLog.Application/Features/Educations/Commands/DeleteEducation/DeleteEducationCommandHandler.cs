using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Educations.Commands.DeleteEducation;

public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, Unit>
{
    private readonly IEducationRecordRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteEducationCommandHandler(IEducationRecordRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        var education = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (education is null)
            throw new NotFoundException($"Education with ID '{request.Id}' was not found.");

        if (education.IsDeleted)
            return Unit.Value;

        await _repo.Update(education, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
