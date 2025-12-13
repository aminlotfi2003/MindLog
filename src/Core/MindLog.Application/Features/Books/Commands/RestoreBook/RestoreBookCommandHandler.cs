using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Books.Commands.RestoreBook;

public class RestoreBookCommandHandler : IRequestHandler<RestoreBookCommand, Unit>
{
    private readonly IBookRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreBookCommandHandler(IBookRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(RestoreBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (book is null)
            throw new NotFoundException($"Book with Id '{request.Id}' was not found.");

        if (!book.IsDeleted)
            return Unit.Value;

        var existsDuplicateActive = await _repo.AnyAsync(
            b => !b.IsDeleted &&
                  b.Id != book.Id &&
                  b.Title == book.Title &&
                  b.AuthorId == book.AuthorId,
            cancellationToken
        );

        if (existsDuplicateActive)
            throw new ConflictException("Another active book with the same title already exists.");

        book.Restore();

        await _repo.Update(book, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
