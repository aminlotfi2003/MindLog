using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfiles;
using MindLog.Application.Features.Skills.Commands.UpdateSkill;
using MindLog.Application.Features.Skills.Dtos;
using MindLog.Application.Features.Skills.Queries.GetSkillDetails;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Skills;

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

        [Display(Name = "نام مهارت"), Required(ErrorMessage = "نام مهارت را وارد کنید."), StringLength(500, ErrorMessage = "نام مهارت نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string Name { get; set; } = default!;

        [Display(Name = "سطح"), Required(ErrorMessage = "سطح مهارت را مشخص کنید.")]
        public SkillLevel Level { get; set; }

        [Display(Name = "دسته‌بندی"), Required(ErrorMessage = "دسته‌بندی مهارت را مشخص کنید.")]
        public SkillCategory Category { get; set; }

        [Display(Name = "ترتیب نمایش"), Range(0, 999, ErrorMessage = "ترتیب نمایش باید بین ۰ تا ۹۹۹ باشد.")]
        public int SortOrder { get; set; }
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IReadOnlyList<ProfileListItemDto> Profiles { get; private set; } = [];
    public SelectList ProfileOptions { get; private set; } = default!;
    public SelectList LevelOptions { get; private set; } = default!;
    public SelectList CategoryOptions { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var details = await _mediator.Send(new GetSkillDetailsQuery(id), cancellationToken);
            Input = new InputModel
            {
                Id = details.Id,
                ProfileId = details.ProfileId,
                Name = details.Name,
                Level = details.Level,
                Category = details.Category,
                SortOrder = details.SortOrder
            };

            await LoadOptionsAsync(cancellationToken);
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
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }

        var dto = new UpdateSkillDto(
            Input.Id,
            Input.ProfileId,
            Input.Name,
            Input.Level,
            Input.Category,
            Input.SortOrder
        );

        var command = new UpdateSkillCommand(
            dto.Id,
            dto.ProfileId,
            dto.Name,
            dto.Level,
            dto.Category,
            dto.SortOrder
        );

        try
        {
            await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "مهارت با موفقیت بروزرسانی شد.";
            return RedirectToPage("Details", new { id = Input.Id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await LoadOptionsAsync(cancellationToken);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
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
        LevelOptions = new SelectList(Enum.GetValues<SkillLevel>().Select(level => new
        {
            Value = level,
            Name = level switch
            {
                SkillLevel.Beginner => "مبتدی",
                SkillLevel.Intermediate => "متوسط",
                SkillLevel.Advanced => "پیشرفته",
                _ => "خبره"
            }
        }), "Value", "Name", Input.Level);
        CategoryOptions = new SelectList(Enum.GetValues<SkillCategory>().Select(category => new
        {
            Value = category,
            Name = category switch
            {
                SkillCategory.Technical => "فنی",
                SkillCategory.SoftSkill => "مهارت نرم",
                SkillCategory.Tooling => "ابزارها",
                _ => "دانش حوزه"
            }
        }), "Value", "Name", Input.Category);
    }
}
