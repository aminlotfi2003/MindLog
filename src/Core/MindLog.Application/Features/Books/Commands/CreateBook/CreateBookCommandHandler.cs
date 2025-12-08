using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Books.Commands.CreateBook;

public sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IBookRepository _bookRepo;
    private readonly IAuthorRepository _authorRepo;
    private readonly IUnitOfWork _uow;

    public CreateBookCommandHandler(
        IBookRepository bookRepo,
        IAuthorRepository authorRepo,
        IUnitOfWork uow)
    {
        _bookRepo = bookRepo;
        _authorRepo = authorRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var authorExists = await _authorRepo.AnyAsync(
            a => a.Id == request.AuthorId,
            cancellationToken
        );
        if (!authorExists)
            throw new NotFoundException($"Author with Id '{request.AuthorId}' was not found.");

        var slugExists = await _bookRepo.AnyAsync(
            b => b.Slug == request.Slug,
            cancellationToken
        );
        if (slugExists)
            throw new ConflictException($"A book with slug '{request.Slug}' already exists.");

        var book = Book.Create(
            request.AuthorId,
            request.Title,
            request.Slug,
            request.CoverImagePath,
            request.Status,
            request.Category,
            request.ShortSummary,
            request.FullReview,
            request.Rating
        );

        await _bookRepo.AddAsync(book, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
