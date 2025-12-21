using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Educations.Commands.CreateEducation;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfiles;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Educations;

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

        [Display(Name = "موسسه آموزشی"), Required(ErrorMessage = "نام موسسه الزامی است."), StringLength(300, ErrorMessage = "نام موسسه نباید بیش از ۳۰۰ کاراکتر باشد.")]
        public string Institution { get; set; } = default!;

        [Display(Name = "مدرک"), Required(ErrorMessage = "مدرک را وارد کنید."), StringLength(200, ErrorMessage = "مدرک نباید بیش از ۲۰۰ کاراکتر باشد.")]
        public string Degree { get; set; } = default!;

        [Display(Name = "رشته"), Required(ErrorMessage = "رشته تحصیلی را وارد کنید."), StringLength(500, ErrorMessage = "رشته نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string FieldOfStudy { get; set; } = default!;

        [Display(Name = "تاریخ شروع"), DataType(DataType.Date)]
        public DateOnly? StartDate { get; set; }

        [Display(Name = "تاریخ پایان"), DataType(DataType.Date)]
        public DateOnly? EndDate { get; set; }

        [Display(Name = "توضیحات"), StringLength(500, ErrorMessage = "توضیحات نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? Description { get; set; }

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

        var command = new CreateEducationCommand(
            Input.ProfileId,
            Input.Institution,
            Input.Degree,
            Input.FieldOfStudy,
            Input.StartDate,
            Input.EndDate,
            Input.Description,
            Input.SortOrder
        );

        try
        {
            var id = await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "سابقه تحصیلی با موفقیت ثبت شد.";
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
