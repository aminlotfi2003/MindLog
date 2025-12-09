using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.Application.Features.Authors.Queries.GetAuthorDetails;

namespace MindLog.WebApp.Pages.Authors;

public class DetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public AuthorDetailsDto Author { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Author = await _mediator.Send(new GetAuthorDetailsQuery(id), cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return Page();
    }
}
