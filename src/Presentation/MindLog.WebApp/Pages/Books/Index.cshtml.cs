using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Books.Commands.DeleteBook;
using MindLog.Application.Features.Books.Dtos;
using MindLog.Application.Features.Books.Queries.GetBooks;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<BookListItemDto> Books { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Books = await _mediator.Send(new GetBooksQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteBookCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "Book deleted successfully.";
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
