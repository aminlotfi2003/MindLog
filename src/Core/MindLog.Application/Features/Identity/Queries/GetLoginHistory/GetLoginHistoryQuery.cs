using MediatR;
using MindLog.Application.Features.Identity.Dtos;

namespace MindLog.Application.Features.Identity.Queries.GetLoginHistory;

public sealed record GetLoginHistoryQuery(Guid UserId, int Count = 20) : IRequest<IReadOnlyList<LoginHistoryDto>>;
