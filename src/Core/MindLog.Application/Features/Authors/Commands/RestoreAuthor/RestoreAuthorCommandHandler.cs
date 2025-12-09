using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Authors.Commands.RestoreAuthor;

public sealed class RestoreAuthorCommandHandler: IRequestHandler<RestoreAuthorCommand, Unit>
{
    private readonly IAuthorRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreAuthorCommandHandler(IAuthorRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(RestoreAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (author is null)
            throw new NotFoundException($"Author with Id '{request.Id}' was not found.");

        if (!author.IsDeleted)
            return Unit.Value;

        author.Restore();

        await _repo.Update(author, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
