using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Courses.Commands.CreateCourse;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfiles;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Courses;

[Authorize(Policy = ApplicationRoles.Admin)]
public class CreateModel : PageModel
{
    private readonly IMediator _mediator;

    public CreateModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class InputModel
    {
        [Display(Name = "پروفایل"), Required(ErrorMessage = "انتخاب پروفایل الزامی است.")]
        public Guid ProfileId { get; set; }

        [Display(Name = "عنوان دوره"), Required(ErrorMessage = "عنوان دوره را وارد کنید."), StringLength(500, ErrorMessage = "عنوان نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string Title { get; set; } = default!;

        [Display(Name = "ارائه‌دهنده"), StringLength(500, ErrorMessage = "نام ارائه‌دهنده نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? Provider { get; set; }

        [Display(Name = "لینک گواهی"), StringLength(500, ErrorMessage = "لینک گواهی نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? CertificateUrl { get; set; }

        [Display(Name = "تاریخ تکمیل"), DataType(DataType.Date)]
        public DateOnly? CompletionDate { get; set; }

        [Display(Name = "ترتیب نمایش"), Range(0, 999, ErrorMessage = "ترتیب نمایش باید بین ۰ تا ۹۹۹ باشد.")]
        public int SortOrder { get; set; }
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IReadOnlyList<ProfileListItemDto> Profiles { get; private set; } = [];
    public SelectList ProfileOptions { get; private set; } = default!;

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        await LoadProfilesAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadProfilesAsync(cancellationToken);
            return Page();
        }

        var command = new CreateCourseCommand(
            Input.ProfileId,
            Input.Title,
            Input.Provider,
            Input.CertificateUrl,
            Input.CompletionDate,
            Input.SortOrder
        );

        try
        {
            var id = await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "دوره آموزشی با موفقیت ثبت شد.";
            return RedirectToPage("Details", new { id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadProfilesAsync(cancellationToken);
            return Page();
        }
        catch (NotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadProfilesAsync(cancellationToken);
            return Page();
        }
    }

    private async Task LoadProfilesAsync(CancellationToken cancellationToken)
    {
        Profiles = await _mediator.Send(new GetProfilesQuery(), cancellationToken);
        ProfileOptions = new SelectList(
            Profiles.Select(p => new { p.Id, p.FullName }),
            "Id",
            "FullName",
            Input.ProfileId);
    }
}
