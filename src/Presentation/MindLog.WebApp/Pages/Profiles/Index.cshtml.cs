using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Features.Profiles.Dtos;
using MindLog.Application.Features.Profiles.Queries.GetProfiles;

namespace MindLog.WebApp.Pages.Profiles;

public class IndexModel : PageModel
{
    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IReadOnlyList<ProfileListItemDto> Profiles { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Profiles = await _mediator.Send(new GetProfilesQuery(), cancellationToken);
    }
}
