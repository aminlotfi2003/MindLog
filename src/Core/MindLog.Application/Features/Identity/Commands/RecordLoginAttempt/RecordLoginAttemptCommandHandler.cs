using MediatR;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Identity;

namespace MindLog.Application.Features.Identity.Commands.RecordLoginAttempt;

public sealed class RecordLoginAttemptCommandHandler(IUserLoginHistoryRepository repository) : IRequestHandler<RecordLoginAttemptCommand>
{
    private readonly IUserLoginHistoryRepository _repository = repository;

    public async Task Handle(RecordLoginAttemptCommand request, CancellationToken cancellationToken)
    {
        var history = new UserLoginHistory
        {
            UserId = request.UserId,
            Success = request.Success,
            FailureCountBeforeSuccess = request.FailureCountBeforeSuccess,
            IpAddress = request.IpAddress,
            Host = request.Host,
            OccurredAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(history, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
