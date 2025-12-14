using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Authors.Commands.CreateAuthor;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Authors;

public class CreateModel : PageModel
{
    private readonly IMediator _mediator;

    public CreateModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class InputModel
    {
        [Required(ErrorMessage = "وارد کردن نام الزامی است."), StringLength(100, ErrorMessage = "نام نباید بیش از ۱۰۰ کاراکتر باشد.")]
        [Display(Name = "نام")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است."), StringLength(100, ErrorMessage = "نام خانوادگی نباید بیش از ۱۰۰ کاراکتر باشد.")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = default!;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        var command = new CreateAuthorCommand(Input.FirstName, Input.LastName);

        try
        {
            var id = await _mediator.Send(command, cancellationToken);
            return RedirectToPage("Details", new { id });
        }
        catch (ConflictException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
