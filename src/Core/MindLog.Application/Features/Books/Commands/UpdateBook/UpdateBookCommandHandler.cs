using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
{
    private readonly IBookRepository _bookRepo;
    private readonly IAuthorRepository _authorRepo;
    private readonly IUnitOfWork _uow;

    public UpdateBookCommandHandler(
        IBookRepository bookRepo,
        IAuthorRepository authorRepo,
        IUnitOfWork uow)
    {
        _bookRepo = bookRepo;
        _authorRepo = authorRepo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepo.GetByIdAsync(request.Id, cancellationToken);
        if (book is null)
            throw new NotFoundException($"Book with Id '{request.Id}' was not found.");

        var authorExists = await _authorRepo.AnyAsync(
            a => a.Id == request.AuthorId,
            cancellationToken
        );
        if (!authorExists)
            throw new NotFoundException($"Author with Id '{request.AuthorId}' was not found.");

        var duplicateSlug = await _bookRepo.AnyAsync(
            b => b.Id != request.Id && b.Slug == request.Slug,
            cancellationToken
        );
        if (duplicateSlug)
            throw new ConflictException($"Another book with slug '{request.Slug}' already exists.");

        book.ChangeAuthor(request.AuthorId);
        book.ModifyReview(
            request.Title,
            request.ShortSummary,
            request.FullReview,
            request.Rating
        );
        book.ChangeStatus(request.Status);
        book.ChangeCategory(request.Category);
        book.Slug = request.Slug;
        book.CoverImagePath = request.CoverImagePath;

        await _bookRepo.Update(book, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
