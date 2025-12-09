using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Authors.Commands.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
{
    private readonly IAuthorRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteAuthorCommandHandler(IAuthorRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (author is null)
            throw new NotFoundException($"Author with Id '{request.Id}' was not found.");

        if (author.IsDeleted)
            return Unit.Value;

        author.Remove();

        await _repo.Update(author, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
