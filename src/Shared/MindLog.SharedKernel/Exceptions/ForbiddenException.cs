namespace MindLog.SharedKernel.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message = "Forbidden")
        : base(message)
    {
    }

    public ForbiddenException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
