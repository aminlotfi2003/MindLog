using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Books.Dtos;
using MindLog.Application.Features.Books.Queries.GetBookDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Books;

public class DetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public BookDetailsDto Book { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Book = await _mediator.Send(new GetBookDetailsQuery(id), cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
