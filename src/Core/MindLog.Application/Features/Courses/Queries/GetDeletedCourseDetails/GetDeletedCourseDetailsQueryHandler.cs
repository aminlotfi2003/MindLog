using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Courses.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Courses.Queries.GetDeletedCourseDetails;

public class GetDeletedCourseDetailsQueryHandler : IRequestHandler<GetDeletedCourseDetailsQuery, CourseDetailsDto>
{
    private readonly ITrainingCourseRepository _repo;

    public GetDeletedCourseDetailsQueryHandler(ITrainingCourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<CourseDetailsDto> Handle(GetDeletedCourseDetailsQuery request, CancellationToken cancellationToken)
    {
        var course = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (course is null)
            throw new NotFoundException($"Course with ID '{request.Id}' was not found.");

        return CourseDetailsDto.FromEntity(course);
    }
}
