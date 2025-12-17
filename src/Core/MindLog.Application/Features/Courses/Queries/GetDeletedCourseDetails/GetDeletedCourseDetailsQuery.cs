using MediatR;
using MindLog.Application.Features.Courses.Dtos;

namespace MindLog.Application.Features.Courses.Queries.GetDeletedCourseDetails;

public record GetDeletedCourseDetailsQuery(Guid Id) : IRequest<CourseDetailsDto>;
