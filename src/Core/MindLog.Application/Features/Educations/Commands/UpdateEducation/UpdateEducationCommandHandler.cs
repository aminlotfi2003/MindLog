using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Educations.Commands.UpdateEducation;

public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, Unit>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly IEducationRecordRepository _educationRepo;
    private readonly IUnitOfWork _uow;

    public UpdateEducationCommandHandler(
        IPersonalProfileRepository profileRepo,
        IEducationRecordRepository educationRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _educationRepo = educationRepo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateEducationCommand request,CancellationToken cancellationToken)
    {
        var course = await _educationRepo.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
            throw new NotFoundException($"Education with ID '{request.Id}' was not found.");

        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with Id '{request.ProfileId}' was not found.");

        var titleExists = await _educationRepo.AnyAsync(
            x => x.FieldOfStudy == request.FieldOfStudy,
            cancellationToken
        );
        if (titleExists)
            throw new ConflictException($"A education with field '{request.FieldOfStudy}' already exists.");

        await _educationRepo.Update(course, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
