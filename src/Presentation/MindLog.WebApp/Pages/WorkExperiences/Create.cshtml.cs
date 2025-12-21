using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfiles;
using MindLog.Application.Features.WorkExperienes.Commands.CreateWorkExperiene;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.WorkExperiences;

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

        [Display(Name = "شرکت"), Required(ErrorMessage = "نام شرکت را وارد کنید."), StringLength(300, ErrorMessage = "نام شرکت نباید بیش از ۳۰۰ کاراکتر باشد.")]
        public string Company { get; set; } = default!;

        [Display(Name = "عنوان نقش"), Required(ErrorMessage = "عنوان نقش را وارد کنید."), StringLength(200, ErrorMessage = "عنوان نقش نباید بیش از ۲۰۰ کاراکتر باشد.")]
        public string RoleTitle { get; set; } = default!;

        [Display(Name = "نوع همکاری"), Required(ErrorMessage = "نوع همکاری را مشخص کنید.")]
        public EmploymentType EmploymentType { get; set; }

        [Display(Name = "نحوه کار"), Required(ErrorMessage = "نحوه کار را مشخص کنید.")]
        public WorkMode WorkMode { get; set; }

        [Display(Name = "تاریخ شروع"), DataType(DataType.Date), Required(ErrorMessage = "تاریخ شروع الزامی است.")]
        public DateOnly StartDate { get; set; }

        [Display(Name = "تاریخ پایان"), DataType(DataType.Date)]
        public DateOnly? EndDate { get; set; }

        [Display(Name = "موقعیت مکانی"), StringLength(500, ErrorMessage = "موقعیت مکانی نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? Location { get; set; }

        [Display(Name = "شرح مسئولیت‌ها"), StringLength(500, ErrorMessage = "شرح مسئولیت‌ها نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? Description { get; set; }

        [Display(Name = "ترتیب نمایش"), Range(0, 999, ErrorMessage = "ترتیب نمایش باید بین ۰ تا ۹۹۹ باشد.")]
        public int SortOrder { get; set; }
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IReadOnlyList<ProfileListItemDto> Profiles { get; private set; } = [];
    public SelectList ProfileOptions { get; private set; } = default!;
    public SelectList EmploymentTypeOptions { get; private set; } = default!;
    public SelectList WorkModeOptions { get; private set; } = default!;

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        await LoadOptionsAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }

        var command = new CreateWorkExperieneCommand(
            Input.ProfileId,
            Input.Company,
            Input.RoleTitle,
            Input.EmploymentType,
            Input.WorkMode,
            Input.StartDate,
            Input.EndDate,
            Input.Location,
            Input.Description,
            Input.SortOrder
        );

        try
        {
            var id = await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "سابقه کاری با موفقیت ثبت شد.";
            return RedirectToPage("Details", new { id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }
        catch (NotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }
    }

    private async Task LoadOptionsAsync(CancellationToken cancellationToken)
    {
        Profiles = await _mediator.Send(new GetProfilesQuery(), cancellationToken);
        ProfileOptions = new SelectList(
            Profiles.Select(p => new { p.Id, p.FullName }),
            "Id",
            "FullName",
            Input.ProfileId);
        EmploymentTypeOptions = new SelectList(Enum.GetValues<EmploymentType>().Select(type => new
        {
            Value = type,
            Name = type switch
            {
                EmploymentType.FullTime => "تمام‌وقت",
                EmploymentType.PartTime => "پاره‌وقت",
                EmploymentType.Contract => "قراردادی",
                EmploymentType.Freelance => "فریلنس",
                _ => "کارآموزی"
            }
        }), "Value", "Name", Input.EmploymentType);
        WorkModeOptions = new SelectList(Enum.GetValues<WorkMode>().Select(mode => new
        {
            Value = mode,
            Name = mode switch
            {
                WorkMode.OnSite => "حضوری",
                WorkMode.Remote => "دورکاری",
                _ => "ترکیبی"
            }
        }), "Value", "Name", Input.WorkMode);
    }
}
