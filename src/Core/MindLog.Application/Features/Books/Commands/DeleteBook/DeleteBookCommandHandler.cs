using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
{
    private readonly IBookRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteBookCommandHandler(IBookRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (book is null)
            throw new NotFoundException($"Book with '{request.Id}' was not found.");

        if (book.IsDeleted)
            return Unit.Value;

        book.Remove();

        await _repo.Update(book, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
