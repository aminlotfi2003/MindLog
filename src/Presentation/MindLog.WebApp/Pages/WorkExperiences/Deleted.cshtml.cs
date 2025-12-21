using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.WorkExperienes.Commands.RestoreWorkExperiene;
using MindLog.Application.Features.WorkExperienes.Dtos;
using MindLog.Application.Features.WorkExperienes.Queries.GetDeletedWorkExperienes;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.WorkExperiences;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<WorkExperieneListItemDto> WorkExperiences { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        WorkExperiences = await _mediator.Send(new GetDeletedWorkExperienesQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostRestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new RestoreWorkExperieneCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "سابقه کاری با موفقیت بازیابی شد.";
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
