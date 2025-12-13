using MediatR;

namespace MindLog.Application.Features.Books.Commands.RestoreBook;

public record RestoreBookCommand(Guid Id) : IRequest<Unit>;
