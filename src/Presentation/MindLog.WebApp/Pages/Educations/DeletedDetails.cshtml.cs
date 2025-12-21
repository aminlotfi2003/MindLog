using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Educations.Dtos;
using MindLog.Application.Features.Educations.Queries.GetDeletedEducationDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Educations;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedDetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedDetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public EducationDetailsDto Education { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Education = await _mediator.Send(new GetDeletedEducationDetailsQuery(id), cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
