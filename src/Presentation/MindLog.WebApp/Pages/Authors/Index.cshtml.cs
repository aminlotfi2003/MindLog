using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Authors.Commands.DeleteAuthor;
using MindLog.Application.Features.Authors.Commands.RestoreAuthor;
using MindLog.Application.Features.Authors.Dtos;
using MindLog.Application.Features.Authors.Queries.GetAuthors;

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
        await _mediator.Send(new DeleteAuthorCommand(id), cancellationToken);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRestoreAsync(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new RestoreAuthorCommand(id), cancellationToken);
        return RedirectToPage();
    }
}
