using MediatR;

namespace MindLog.Application.Features.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand(Guid Id) : IRequest<Unit>;
