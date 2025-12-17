using MediatR;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Queries.GetDeletedCourses;

public record GetDeletedCoursesQuery : IRequest<IReadOnlyList<CourseListItemDto>>;
