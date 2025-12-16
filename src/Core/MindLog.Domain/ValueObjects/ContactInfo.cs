namespace MindLog.Domain.ValueObjects;

public sealed record ContactInfo(
    string? Email,
    string? PhoneNumber,
    string? Website,
    string? LinkedInUrl,
    string? GitHubUrl)
{
    public static ContactInfo Create(
        string? email = null,
        string? phoneNumber = null,
        string? website = null,
        string? linkedInUrl = null,
        string? gitHubUrl = null)
    {
        return new ContactInfo(
            email?.Trim(),
            phoneNumber?.Trim(),
            website?.Trim(),
            linkedInUrl?.Trim(),
            gitHubUrl?.Trim()
        );
    }
}
