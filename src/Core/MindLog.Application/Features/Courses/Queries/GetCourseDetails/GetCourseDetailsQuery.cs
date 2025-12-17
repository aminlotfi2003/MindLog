using MediatR;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Queries.GetCourseDetails;

public record GetCourseDetailsQuery(Guid Id) : IRequest<CourseDetailsDto>;
