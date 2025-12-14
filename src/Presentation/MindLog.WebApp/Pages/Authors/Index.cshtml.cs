using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Authors.Commands.DeleteAuthor;
using MindLog.Application.Features.Authors.Commands.RestoreAuthor;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.Application.Features.Authors.Queries.GetAuthors;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.WebApp.Pages.Authors;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<AuthorListItemDto> Authors { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Authors = await _mediator.Send(new GetAuthorsQuery(), cancellationToken);
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteAuthorCommand(id), cancellationToken);
            TempData["SuccessMessage"] = "نویسنده با موفقیت حذف شد.";
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToPage();
    }
}
