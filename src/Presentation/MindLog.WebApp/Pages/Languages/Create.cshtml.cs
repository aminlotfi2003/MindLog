using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Languages.Commands.CreateLanguage;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfiles;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Languages;

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

        [Display(Name = "زبان"), Required(ErrorMessage = "نام زبان را وارد کنید."), StringLength(100, ErrorMessage = "نام زبان نباید بیش از ۱۰۰ کاراکتر باشد.")]
        public string Language { get; set; } = default!;

        [Display(Name = "سطح"), Required(ErrorMessage = "سطح زبان را مشخص کنید.")]
        public LanguageLevel Level { get; set; }

        [Display(Name = "گواهی"), StringLength(200, ErrorMessage = "گواهی نباید بیش از ۲۰۰ کاراکتر باشد.")]
        public string? Certificate { get; set; }

        [Display(Name = "ترتیب نمایش"), Range(0, 999, ErrorMessage = "ترتیب نمایش باید بین ۰ تا ۹۹۹ باشد.")]
        public int SortOrder { get; set; }
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IReadOnlyList<ProfileListItemDto> Profiles { get; private set; } = [];
    public SelectList ProfileOptions { get; private set; } = default!;
    public SelectList LevelOptions { get; private set; } = default!;

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

        var command = new CreateLanguageCommand(
            Input.ProfileId,
            Input.Language,
            Input.Level,
            Input.Certificate,
            Input.SortOrder
        );

        try
        {
            var id = await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "مهارت زبانی با موفقیت ثبت شد.";
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
        LevelOptions = new SelectList(Enum.GetValues<LanguageLevel>().Select(level => new
        {
            Value = level,
            Name = level switch
            {
                LanguageLevel.A1 => "A1 - پایه",
                LanguageLevel.A2 => "A2 - ابتدایی",
                LanguageLevel.B1 => "B1 - متوسط",
                LanguageLevel.B2 => "B2 - خوب",
                LanguageLevel.C1 => "C1 - پیشرفته",
                LanguageLevel.C2 => "C2 - حرفه‌ای",
                _ => "زبان مادری"
            }
        }), "Value", "Name", Input.Level);
    }
}
