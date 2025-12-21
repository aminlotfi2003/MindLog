using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Courses.Commands.DeleteCourse;
using MindLog.Application.Features.Courses.Dtos;
using MindLog.Application.Features.Courses.Queries.GetCourses;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<CourseListItemDto> Courses { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Courses = await _mediator.Send(new GetCoursesQuery(), cancellationToken);
    }

    [Authorize(Policy = ApplicationRoles.Admin)]
    public async Task<IActionResult> OnPostDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteCourseCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "دوره آموزشی با موفقیت حذف شد.";
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
