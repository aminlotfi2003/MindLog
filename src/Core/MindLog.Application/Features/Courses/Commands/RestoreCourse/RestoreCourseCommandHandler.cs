using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Courses.Commands.RestoreCourse;

public class RestoreCourseCommandHandler : IRequestHandler<RestoreCourseCommand, Unit>
{
    private readonly ITrainingCourseRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreCourseCommandHandler(ITrainingCourseRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(RestoreCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (course is null)
            throw new NotFoundException($"Course with ID '{request.Id}' was not found.");

        if (!course.IsDeleted)
            return Unit.Value;

        var existsDuplicateActive = await _repo.AnyAsync(
            b => !b.IsDeleted &&
                  b.Id != course.Id &&
                  b.Title == course.Title &&
                  b.ProfileId == course.ProfileId,
            cancellationToken
        );

        if (existsDuplicateActive)
            throw new ConflictException("Another active course with the same title already exists.");

        await _repo.Update(course, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
