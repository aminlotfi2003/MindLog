using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Courses.Commands.RestoreCourse;
using MindLog.Application.Features.Courses.Dtos;
using MindLog.Application.Features.Courses.Queries.GetDeletedCourses;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Courses;

[Authorize(Policy = ApplicationRoles.Admin)]
public class DeletedModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<CourseListItemDto> Courses { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Courses = await _mediator.Send(new GetDeletedCoursesQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostRestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new RestoreCourseCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "دوره آموزشی با موفقیت بازیابی شد.";
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
