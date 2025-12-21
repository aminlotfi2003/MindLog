using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Profiles.Commands.UpdateProfile;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfileDetails;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Profiles;

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

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var details = await _mediator.Send(new GetProfileDetailsQuery(id), cancellationToken);
            Input = new InputModel
            {
                Id = details.Id,
                FullName = details.FullName,
                Summary = details.Summary,
                BirthDate = details.BirthDate,
                Email = details.Email,
                PhoneNumber = details.PhoneNumber,
                Website = details.Website,
                LinkedInUrl = details.LinkedInUrl,
                GitHubUrl = details.GitHubUrl,
                Address = details.Address
            };

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
            return Page();
        }

        var dto = new UpdateProfileDto(
            Input.Id,
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

        var command = UpdateProfileCommand.FromEntity(dto);

        try
        {
            await _mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = "پروفایل با موفقیت بروزرسانی شد.";
            return RedirectToPage("Details", new { id = Input.Id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
