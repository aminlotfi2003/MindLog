using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Skills.Commands.DeleteSkill;
using MindLog.Application.Features.Skills.Dtos;
using MindLog.Application.Features.Skills.Queries.GetSkills;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Skills;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<SkillListItemDto> Skills { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Skills = await _mediator.Send(new GetSkillsQuery(), cancellationToken);
    }

    [Authorize(Policy = ApplicationRoles.Admin)]
    public async Task<IActionResult> OnPostDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteSkillCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "مهارت با موفقیت حذف شد.";
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
