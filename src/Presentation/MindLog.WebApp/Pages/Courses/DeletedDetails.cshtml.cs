using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Courses.Dtos;
using MindLog.Application.Features.Courses.Queries.GetDeletedCourseDetails;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Courses;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedDetailsModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedDetailsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CourseDetailsDto Course { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            Course = await _mediator.Send(new GetDeletedCourseDetailsQuery(id), cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
