using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IAuthorRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateAuthorCommandHandler(IAuthorRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        bool exists = await _repo.AnyAsync(
            a => a.FirstName == request.FirstName
              && a.LastName == request.LastName,
            cancellationToken
        );

        if (exists)
            throw new ConflictException("Author with the same name already exists.");

        var author = Author.Create(request.FirstName, request.LastName);
        await _repo.AddAsync(author, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}
