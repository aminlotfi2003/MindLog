namespace MindLog.SharedKernel.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, string? errorCode)
        : base(message, errorCode)
    {
    }

    public NotFoundException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
