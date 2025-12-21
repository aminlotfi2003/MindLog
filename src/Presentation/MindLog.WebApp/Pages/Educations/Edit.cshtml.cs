using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Educations.Commands.UpdateEducation;
using MindLog.Application.Features.Educations.Dtos;
using MindLog.Application.Features.Educations.Queries.GetEducationDetails;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfiles;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Educations;

[Authorize(Policy = ApplicationRoles.Admin)]
public class EditModel : PageModel
{
    private readonly IMediator _mediator;

    public EditModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class InputModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

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

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var details = await _mediator.Send(new GetEducationDetailsQuery(id), cancellationToken);
            Input = new InputModel
            {
                Id = details.Id,
                ProfileId = details.ProfileId,
                Institution = details.Institution,
                Degree = details.Degree,
                FieldOfStudy = details.FieldOfStudy,
                StartDate = details.StartDate,
                EndDate = details.EndDate,
                Description = details.Description,
                SortOrder = details.SortOrder
            };

            await LoadProfilesAsync(cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadProfilesAsync(cancellationToken);
            return Page();
        }

        var dto = new UpdateEducationDto(
            Input.Id,
            Input.ProfileId,
            Input.Institution,
            Input.Degree,
            Input.FieldOfStudy,
            Input.StartDate,
            Input.EndDate,
            Input.Description,
            Input.SortOrder
        );

        var command = new UpdateEducationCommand(
            dto.Id,
            dto.ProfileId,
            dto.Institution,
            dto.Degree,
            dto.FieldOfStudy,
            dto.StartDate,
            dto.EndDate,
            dto.Description,
            dto.SortOrder
        );

        try
        {
            await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "سابقه تحصیلی با موفقیت بروزرسانی شد.";
            return RedirectToPage("Details", new { id = Input.Id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadProfilesAsync(cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
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
