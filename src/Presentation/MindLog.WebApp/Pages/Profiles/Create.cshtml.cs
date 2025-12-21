using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Profiles.Commands.CreateProfile;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Profiles;

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
        [Display(Name = "نام کامل"), Required(ErrorMessage = "نام کامل را وارد کنید."), StringLength(100, ErrorMessage = "نام کامل نباید بیش از ۱۰۰ کاراکتر باشد.")]
        public string FullName { get; set; } = default!;

        [Display(Name = "خلاصه حرفه‌ای"), StringLength(1000, ErrorMessage = "خلاصه نباید بیش از ۱۰۰۰ کاراکتر باشد.")]
        public string? Summary { get; set; }

        [Display(Name = "تاریخ تولد"), DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; }

        [Display(Name = "ایمیل"), EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست."), StringLength(500, ErrorMessage = "ایمیل نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? Email { get; set; }

        [Display(Name = "شماره تماس"), StringLength(50, ErrorMessage = "شماره تماس نباید بیش از ۵۰ کاراکتر باشد.")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "وب‌سایت"), Url(ErrorMessage = "آدرس وب‌سایت صحیح نیست."), StringLength(500, ErrorMessage = "وب‌سایت نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? Website { get; set; }

        [Display(Name = "لینکدین"), Url(ErrorMessage = "آدرس لینکدین صحیح نیست."), StringLength(500, ErrorMessage = "لینکدین نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? LinkedInUrl { get; set; }

        [Display(Name = "گیت‌هاب"), Url(ErrorMessage = "آدرس گیت‌هاب صحیح نیست."), StringLength(500, ErrorMessage = "گیت‌هاب نباید بیش از ۵۰۰ کاراکتر باشد.")]
        public string? GitHubUrl { get; set; }

        [Display(Name = "آدرس"), StringLength(100, ErrorMessage = "آدرس نباید بیش از ۱۰۰ کاراکتر باشد.")]
        public string? Address { get; set; }
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var command = new CreateProfileCommand(
            Input.FullName,
            Input.Summary,
            Input.BirthDate,
            Input.Email,
            Input.PhoneNumber,
            Input.Website,
            Input.LinkedInUrl,
            Input.GitHubUrl,
            Input.Address
        );

        try
        {
            var id = await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "پروفایل جدید با موفقیت ایجاد شد.";
            return RedirectToPage("Details", new { id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
