using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Skills.Commands.RestoreSkill;
using MindLog.Application.Features.Skills.Dtos;
using MindLog.Application.Features.Skills.Queries.GetDeletedSkills;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Skills;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<SkillListItemDto> Skills { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Skills = await _mediator.Send(new GetDeletedSkillsQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostRestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new RestoreSkillCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "مهارت با موفقیت بازیابی شد.";
        }
        catch (ConflictException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
