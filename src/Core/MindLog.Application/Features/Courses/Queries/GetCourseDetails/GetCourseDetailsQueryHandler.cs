using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Courses.Dtos;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Courses.Queries.GetCourseDetails;

public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailsDto>
{
    private readonly ITrainingCourseRepository _repo;

    public GetCourseDetailsQueryHandler(ITrainingCourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<CourseDetailsDto> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
    {
        var course = await _repo.GetByIdAsync(
            request.Id,
            cancellationToken
        );

        if (course is null)
            throw new NotFoundException($"Course with Id '{request.Id}' was not found.");

        return CourseDetailsDto.FromEntity(course);
    }
}
