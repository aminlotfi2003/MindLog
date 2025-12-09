using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Authors.Commands.RestoreAuthor;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.Application.Features.Authors.Queries.GetDeletedAuthors;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Authors;

public class DeletedModel : PageModel
{
    private readonly IMediator _mediator;

    public DeletedModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<AuthorListItemDto> Authors { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Authors = await _mediator.Send(new GetDeletedAuthorsQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostRestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new RestoreAuthorCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "Author restored successfully.";
        }
        catch (ConflictException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
