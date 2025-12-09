using MediatR;

namespace MindLog.Application.Features.Authors.Commands.DeleteAuthor;

public sealed record DeleteAuthorCommand(Guid Id) : IRequest<Unit>;
