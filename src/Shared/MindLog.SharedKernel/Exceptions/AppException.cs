namespace MindLog.SharedKernel.Exceptions;

public abstract class AppException : Exception
{
    public string? ErrorCode { get; }

    public IDictionary<string, string[]> Errors { get; }

    protected AppException(string message)
        : this(message, null, null, null)
    {
    }

    protected AppException(string message, Exception? innerException)
        : this(message, null, null, innerException)
    {
    }

    protected AppException(string message, string? errorCode)
        : this(message, errorCode, null, null)
    {
    }

    protected AppException(string message, string? errorCode, Exception? innerException)
        : this(message, errorCode, null, innerException)
    {
    }

    protected AppException(
        string message,
        string? errorCode,
        IDictionary<string, string[]>? errors,
        Exception? innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        Errors = errors ?? new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
    }
}
