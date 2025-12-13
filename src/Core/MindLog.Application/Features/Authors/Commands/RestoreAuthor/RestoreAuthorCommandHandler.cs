using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Authors.Commands.RestoreAuthor;

public sealed class RestoreAuthorCommandHandler : IRequestHandler<RestoreAuthorCommand, Unit>
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
        var author = await _repo.GetByIdIncludingDeletedAsync(request.Id, cancellationToken);

        if (author is null)
            throw new NotFoundException($"Author with Id '{request.Id}' was not found.");

        if (!author.IsDeleted)
            return Unit.Value;

        var existsDuplicateActive = await _repo.AnyAsync(
            a => !a.IsDeleted &&
                  a.Id != author.Id &&
                  a.FirstName == author.FirstName &&
                  a.LastName == author.LastName,
            cancellationToken
        );

        if (existsDuplicateActive)
            throw new ConflictException("Another active author with the same name already exists.");

        author.Restore();

        await _repo.Update(author, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
