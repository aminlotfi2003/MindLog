using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.WorkExperienes.Commands.DeleteWorkExperiene;
using MindLog.Application.Features.WorkExperienes.Dtos;
using MindLog.Application.Features.WorkExperienes.Queries.GetWorkExperienes;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.WorkExperiences;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<WorkExperieneListItemDto> WorkExperiences { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        WorkExperiences = await _mediator.Send(new GetWorkExperienesQuery(), cancellationToken);
    }

    [Authorize(Policy = ApplicationRoles.Admin)]
    public async Task<IActionResult> OnPostDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteWorkExperieneCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "سابقه کاری با موفقیت حذف شد.";
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
