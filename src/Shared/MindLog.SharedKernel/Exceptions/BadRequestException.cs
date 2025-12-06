namespace MindLog.SharedKernel.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, string? errorCode)
        : base(message, errorCode)
    {
    }

    public BadRequestException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public BadRequestException(string message, IDictionary<string, string[]> errors)
        : base(message, null, errors, null)
    {
    }
}
