using FluentValidation.Results;

namespace MindLog.SharedKernel.Exceptions;

public class ValidationException : BadRequestException
{
    public ValidationException(string message)
        : base(message)
    {
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("Validation failed.", BuildErrors(failures))
    {
    }

    private static IDictionary<string, string[]> BuildErrors(IEnumerable<ValidationFailure> failures)
    {
        return failures
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                grouping => grouping.Key,
                grouping => grouping.Select(failure => failure.ErrorMessage).ToArray(),
                StringComparer.OrdinalIgnoreCase
            );
    }
}
