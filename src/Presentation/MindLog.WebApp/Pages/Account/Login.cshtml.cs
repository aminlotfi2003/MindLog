using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Identity.Commands.AdminLogin;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly IMediator _mediator;

    public LoginModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string? ReturnUrl { get; set; }

    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl ?? Url.Page("/Index");
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null, CancellationToken cancellationToken = default)
    {
        returnUrl ??= Url.Page("/Index");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var host = HttpContext.Request.Host.Value;

        var loginResult = await _mediator.Send(new AdminLoginCommand(
            Input.Email,
            Input.Password,
            Input.RememberMe,
            ipAddress,
            host), cancellationToken);

        if (loginResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        if (loginResult.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, loginResult.ErrorMessage ?? "حساب شما به دلیل تلاش‌های ناموفق قفل شده است.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, loginResult.ErrorMessage ?? "اطلاعات ورود اشتباه است.");
        }

        return Page();
    }

    public class InputModel
    {
        [Required(ErrorMessage = "ایمیل الزامی است.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر وارد کنید.")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
