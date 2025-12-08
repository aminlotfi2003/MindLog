using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Unit>
{
    private readonly IAuthorRepository _repo;
    private readonly IUnitOfWork _uow;

    public UpdateAuthorCommandHandler(IAuthorRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (author is null)
            throw new NotFoundException($"Author with Id '{request.Id}' was not found.");

        var duplicateExists = await _repo.AnyAsync(
            a => a.Id != request.Id &&
                 a.FirstName == request.FirstName &&
                 a.LastName == request.LastName,
            cancellationToken
        );

        if (duplicateExists)
            throw new ConflictException("Another author with the same name already exists.");

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;

        await _repo.Update(author, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
