using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.WorkExperienes.Dtos;
using MindLog.Application.Features.WorkExperienes.Queries.GetDeletedWorkExperieneDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.WorkExperiences;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedDetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedDetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public WorkExperieneDetailsDto WorkExperience { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            WorkExperience = await _mediator.Send(new GetDeletedWorkExperieneDetailsQuery(id), cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
