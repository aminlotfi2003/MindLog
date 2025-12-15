using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Application.Features.Identity.Dtos;

namespace MindLog.Application.Features.Identity.Queries.GetLoginHistory;

public sealed class GetLoginHistoryQueryHandler(IUserLoginHistoryRepository repository)
    : IRequestHandler<GetLoginHistoryQuery, IReadOnlyList<LoginHistoryDto>>
{
    private readonly IUserLoginHistoryRepository _repository = repository;

    public async Task<IReadOnlyList<LoginHistoryDto>> Handle(GetLoginHistoryQuery request, CancellationToken cancellationToken)
    {
        var histories = await _repository.GetRecentAsync(request.UserId, request.Count, cancellationToken);

        return histories
            .Select(history => new LoginHistoryDto(
                history.OccurredAt,
                history.IpAddress,
                history.Host,
                history.Success,
                history.FailureCountBeforeSuccess))
            .ToList();
    }
}
