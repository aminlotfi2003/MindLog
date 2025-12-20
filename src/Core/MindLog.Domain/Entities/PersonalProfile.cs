using MindLog.Domain.Common;
using MindLog.Domain.ValueObjects;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class PersonalProfile : EntityBase<Guid>, IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }

    // Header / Summary
    public string FullName { get; private set; } = default!;
    public string? Summary { get; private set; }
    public DateOnly? BirthDate { get; private set; }

    // Contact & Address
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Website { get; private set; }
    public string? LinkedInUrl { get; private set; }
    public string? GitHubUrl { get; private set; }
    public string? Address { get; private set; }

    // Profile image metadata
    public ProfileImage? Image { get; private set; }

    // Collections
    private readonly List<WorkExperience> _workExperiences = new();
    public IReadOnlyCollection<WorkExperience> WorkExperiences => _workExperiences.AsReadOnly();

    private readonly List<EducationRecord> _educations = new();
    public IReadOnlyCollection<EducationRecord> Educations => _educations.AsReadOnly();

    private readonly List<TrainingCourse> _courses = new();
    public IReadOnlyCollection<TrainingCourse> Courses => _courses.AsReadOnly();

    private readonly List<SkillItem> _skills = new();
    public IReadOnlyCollection<SkillItem> Skills => _skills.AsReadOnly();

    private readonly List<LanguageProficiency> _languages = new();
    public IReadOnlyCollection<LanguageProficiency> Languages => _languages.AsReadOnly();

    private PersonalProfile() { }

    public static PersonalProfile Create(
        string fullName,
        string? summary = null,
        DateOnly? BirthDate = null,
        string? email = null,
        string? phoneNumber = null,
        string? website = null,
        string? linkedInUrl = null,
        string? gitHubUrl = null,
        string? address = null)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));

        return new PersonalProfile
        {
            FullName = fullName.Trim(),
            Summary = summary?.Trim(),
            BirthDate = BirthDate,
            Email = email?.Trim(),
            PhoneNumber = phoneNumber?.Trim(),
            Website = website?.Trim(),
            LinkedInUrl = linkedInUrl?.Trim(),
            GitHubUrl = gitHubUrl?.Trim(),
            Address = address
        };
    }

    public void UpdateHeader(string fullName, string? headline, string? summary)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));

        FullName = fullName.Trim();
        Summary = summary?.Trim();
    }

    public void UpdateContact(
        string? email = null,
        string? phoneNumber = null,
        string? website = null,
        string? linkedInUrl = null,
        string? gitHubUrl = null)
    {
        Email = email?.Trim();
        PhoneNumber = phoneNumber?.Trim();
        Website = website?.Trim();
        LinkedInUrl = linkedInUrl?.Trim();
        GitHubUrl = gitHubUrl?.Trim();
    }

    public void UpdateAddress(string? address) => Address = address;

    public void SetProfileImage(ProfileImage image) => Image = image;

    public void RemoveProfileImage() => Image = null;

    // Add/Update behaviors
    public void AddWorkExperience(WorkExperience item) => _workExperiences.Add(item);
    public void AddEducation(EducationRecord item) => _educations.Add(item);
    public void AddCourse(TrainingCourse item) => _courses.Add(item);
    public void AddSkill(SkillItem item) => _skills.Add(item);
    public void AddLanguage(LanguageProficiency item) => _languages.Add(item);
}
