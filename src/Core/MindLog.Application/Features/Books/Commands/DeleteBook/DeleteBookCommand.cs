using MediatR;

namespace MindLog.Application.Features.Books.Commands.DeleteBook;

public record DeleteBookCommand(Guid Id) : IRequest<Unit>;
