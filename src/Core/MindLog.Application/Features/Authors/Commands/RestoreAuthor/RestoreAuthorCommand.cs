using MediatR;

namespace MindLog.Application.Features.Authors.Commands.RestoreAuthor;

public sealed record RestoreAuthorCommand(Guid Id) : IRequest<Unit>;
