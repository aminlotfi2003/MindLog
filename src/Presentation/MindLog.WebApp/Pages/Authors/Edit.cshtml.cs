using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Authors.Commands.UpdateAuthor;
using MindLog.Application.Features.Authors.Queries.GetAuthorDetails;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Authors;

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

        [Required(ErrorMessage = "وارد کردن نام الزامی است."), StringLength(100, ErrorMessage = "نام نباید بیش از ۱۰۰ کاراکتر باشد.")]
        [Display(Name = "نام")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است."), StringLength(100, ErrorMessage = "نام خانوادگی نباید بیش از ۱۰۰ کاراکتر باشد.")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = default!;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var details = await _mediator.Send(new GetAuthorDetailsQuery(id), cancellationToken);

            Input = new InputModel
            {
                Id = details.Id,
                FirstName = details.FirstName,
                LastName = details.LastName
            };

            return Page();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        var command = new UpdateAuthorCommand(Input.Id, Input.FirstName, Input.LastName);

        try
        {
            await _mediator.Send(command, cancellationToken);
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
