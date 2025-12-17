using MediatR;

namespace MindLog.Application.Features.Courses.Commands.RestoreCourse;

public record RestoreCourseCommand(Guid Id) : IRequest<Unit>;
