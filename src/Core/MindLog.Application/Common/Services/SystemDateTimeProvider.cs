using MindLog.Application.Common.Abstractions.Services;

namespace MindLog.Application.Common.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
