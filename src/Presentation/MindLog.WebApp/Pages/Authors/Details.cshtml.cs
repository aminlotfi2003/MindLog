using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.Application.Features.Authors.Queries.GetAuthorDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Authors;

[Authorize(Policy = ApplicationRoles.Admin)]
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
        catch (NotFoundException)
        {
            return NotFound();
        }

        return Page();
    }
}
