using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly ITrainingCourseRepository _courseRepo;
    private readonly IUnitOfWork _uow;

    public UpdateCourseCommandHandler(
        IPersonalProfileRepository profileRepo,
        ITrainingCourseRepository courseRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _courseRepo = courseRepo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepo.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
            throw new NotFoundException($"Course with ID '{request.Id}' was not found.");

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

        await _courseRepo.Update(course, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
