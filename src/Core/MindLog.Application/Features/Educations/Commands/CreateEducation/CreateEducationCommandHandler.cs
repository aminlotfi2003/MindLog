using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Educations.Commands.CreateEducation;

public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, Guid>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly IEducationRecordRepository _educationRepo;
    private readonly IUnitOfWork _uow;

    public CreateEducationCommandHandler(
        IPersonalProfileRepository profileRepo,
        IEducationRecordRepository educationRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _educationRepo = educationRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
    {
        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with Id '{request.ProfileId}' was not found.");

        var fieldExists = await _educationRepo.AnyAsync(
            x => x.FieldOfStudy == request.FieldOfStudy,
            cancellationToken
        );
        if (fieldExists)
            throw new ConflictException($"A education with field '{request.FieldOfStudy}' already exists.");

        var education = EducationRecord.Create(
            request.ProfileId,
            request.Institution,
            request.Degree,
            request.FieldOfStudy,
            request.StartDate,
            request.EndDate,
            request.Description,
            request.SortOrder
        );

        await _educationRepo.AddAsync(education, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return education.Id;
    }
}
