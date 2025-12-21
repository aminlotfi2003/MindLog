using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Educations.Dtos;
using MindLog.Application.Features.Educations.Queries.GetEducationDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Educations;

public class DetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public EducationDetailsDto Education { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Education = await _mediator.Send(new GetEducationDetailsQuery(id), cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
