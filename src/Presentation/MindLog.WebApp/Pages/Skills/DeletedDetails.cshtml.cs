using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Skills.Dtos;
using MindLog.Application.Features.Skills.Queries.GetDeletedSkillDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Skills;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedDetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedDetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public SkillDetailsDto Skill { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Skill = await _mediator.Send(new GetDeletedSkillDetailsQuery(id), cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
