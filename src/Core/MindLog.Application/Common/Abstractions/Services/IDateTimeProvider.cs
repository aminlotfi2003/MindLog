namespace MindLog.Application.Common.Abstractions.Services;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
