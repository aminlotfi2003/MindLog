using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Queries.GetCourses;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IReadOnlyList<CourseListItemDto>>
{
    private readonly ITrainingCourseRepository _repo;

    public GetCoursesQueryHandler(ITrainingCourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<CourseListItemDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _repo.ListAsync(cancellationToken);

        return courses.Select(CourseListItemDto.FromEntity).ToList();
    }
}
