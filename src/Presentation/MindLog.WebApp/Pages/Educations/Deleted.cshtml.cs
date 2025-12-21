using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Educations.Commands.RestoreEducation;
using MindLog.Application.Features.Educations.Dtos;
using MindLog.Application.Features.Educations.Queries.GetDeletedEducations;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Educations;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<EducationListItemDto> Educations { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Educations = await _mediator.Send(new GetDeletedEducationsQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostRestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new RestoreEducationCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "سابقه تحصیلی با موفقیت بازیابی شد.";
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
