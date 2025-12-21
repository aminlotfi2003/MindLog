using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.WorkExperienes.Dtos;
using MindLog.Application.Features.WorkExperienes.Queries.GetWorkExperieneDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.WorkExperiences;

public class DetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public WorkExperieneDetailsDto WorkExperience { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            WorkExperience = await _mediator.Send(new GetWorkExperieneDetailsQuery(id), cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
