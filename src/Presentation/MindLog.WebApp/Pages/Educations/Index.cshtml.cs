using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Educations.Commands.DeleteEducation;
using MindLog.Application.Features.Educations.Dtos;
using MindLog.Application.Features.Educations.Queries.GetEducations;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Educations;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<EducationListItemDto> Educations { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Educations = await _mediator.Send(new GetEducationsQuery(), cancellationToken);
    }

    [Authorize(Policy = ApplicationRoles.Admin)]
    public async Task<IActionResult> OnPostDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteEducationCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "سابقه تحصیلی با موفقیت حذف شد.";
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
