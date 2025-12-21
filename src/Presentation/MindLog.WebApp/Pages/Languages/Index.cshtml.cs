using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Languages.Dtos;
using MindLog.Application.Features.Languages.Queries.GetLanguages;

namespace MindLog.WebApp.Pages.Languages;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<LanguageListItemDto> Languages { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Languages = await _mediator.Send(new GetLanguagesQuery(), cancellationToken);
    }
}
