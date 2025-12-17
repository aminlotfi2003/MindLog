using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Queries.GetDeletedCourses;

public class GetDeletedCoursesQueryHandler : IRequestHandler<GetDeletedCoursesQuery, IReadOnlyList<CourseListItemDto>>
{
    private readonly ITrainingCourseRepository _repo;

    public GetDeletedCoursesQueryHandler(ITrainingCourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<CourseListItemDto>> Handle(GetDeletedCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _repo.ListIncludingDeletedAsync(cancellationToken);

        var deleted = courses
            .Where(x => x.IsDeleted)
            .Select(CourseListItemDto.FromEntity)
            .ToList();

        return deleted;
    }
}
