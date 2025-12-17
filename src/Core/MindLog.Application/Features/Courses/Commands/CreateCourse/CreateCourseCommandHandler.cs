using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly ITrainingCourseRepository _courseRepo;
    private readonly IUnitOfWork _uow;

    public CreateCourseCommandHandler(
        IPersonalProfileRepository profileRepo,
        ITrainingCourseRepository courseRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _courseRepo = courseRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with Id '{request.ProfileId}' was not found.");

        var titleExists = await _courseRepo.AnyAsync(
            x => x.Title == request.Title,
            cancellationToken
        );
        if (titleExists)
            throw new ConflictException($"A course with title '{request.Title}' already exists.");

        var course = TrainingCourse.Create(
            request.ProfileId,
            request.Title,
            request.Provider,
            request.CompletionDate,
            request.CertificateUrl,
            request.SortOrder
        );

        await _courseRepo.AddAsync(course, cancellationToken);
        await _uow.SaveChangesAsync();

        return course.Id;
    }
}
