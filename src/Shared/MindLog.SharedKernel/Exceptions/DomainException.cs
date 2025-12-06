namespace MindLog.SharedKernel.Exceptions;

public class DomainException : AppException
{
    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, string? errorCode)
        : base(message, errorCode)
    {
    }

    public DomainException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
