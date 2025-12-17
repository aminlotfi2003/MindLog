using MediatR;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Queries.GetCourses;

public record GetCoursesQuery : IRequest<IReadOnlyList<CourseListItemDto>>;
