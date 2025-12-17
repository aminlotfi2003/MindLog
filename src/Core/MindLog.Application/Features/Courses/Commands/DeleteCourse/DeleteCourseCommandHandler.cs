using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly ITrainingCourseRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteCourseCommandHandler(ITrainingCourseRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (course is null)
            throw new NotFoundException($"Course with '{request.Id}' was not found.");

        if (course.IsDeleted)
            return Unit.Value;

        await _repo.Update(course, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
