using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Authors.Commands.UpdateAuthor;
using MindLog.Application.Features.Authors.Queries.GetAuthorDetails;
using MindLog.SharedKernel.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MindLog.WebApp.Pages.Authors;

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

        [Required, StringLength(100)]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = default!;

        [Required, StringLength(100)]
        [Display(Name = "Last name")]
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
