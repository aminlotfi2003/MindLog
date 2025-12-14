using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Books.Commands.RestoreBook;
using MindLog.Application.Features.Books.Dtos;
using MindLog.Application.Features.Books.Queries.GetDeletedBooks;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Books;

public class DeletedModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<BookListItemDto> Books { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Books = await _mediator.Send(new GetDeletedBooksQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostRestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new RestoreBookCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "کتاب با موفقیت بازیابی شد.";
        }
        catch (ConflictException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
