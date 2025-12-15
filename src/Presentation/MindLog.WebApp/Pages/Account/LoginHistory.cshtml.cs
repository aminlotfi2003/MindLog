using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindLog.Application.Common.Models;
using MindLog.Application.Features.Identity.Dtos;
using MindLog.Application.Features.Identity.Queries.GetLoginHistory;
using MindLog.Domain.Identity;

namespace MindLog.WebApp.Pages.Account;

[Authorize(Policy = ApplicationRoles.Admin)]
public class LoginHistoryModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginHistoryModel(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public IReadOnlyList<LoginHistoryDto> History { get; private set; } = [];

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(User) ?? throw new InvalidOperationException("کاربر یافت نشد.");

        var userId = await _userManager.GetUserIdAsync(user)
                     ?? throw new InvalidOperationException("شناسه کاربر یافت نشد.");

        var parsedId = Guid.Parse(userId);
        History = await _mediator.Send(new GetLoginHistoryQuery(parsedId, 50), cancellationToken);
    }
}
