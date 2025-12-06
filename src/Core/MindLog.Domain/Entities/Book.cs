using MindLog.Domain.Common;
using MindLog.Domain.Enums;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class Book : EntityBase<Guid>, IAuditableEntity, ISoftDeletable
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string CoverImagePath { get; set; } = default!;
    public ReadingStatus Status { get; set; }
    public BookCategory Category { get; set; }
    public string ShortSummary { get; set; } = default!;
    public string FullReview { get; set; } = default!;
    public int? Rating { get; set; } // 1-5

    public Book() { } // for EF

    public static Book Create(
        Guid authorId,
        string title,
        string slug,
        string coverImagePath,
        ReadingStatus status,
        BookCategory category,
        string shortSummary,
        string fullReview,
        int? rating)
    {
        var book = new Book
        {
            AuthorId = authorId,
            Title = title,
            Slug = slug,
            CoverImagePath = coverImagePath,
            Status = status,
            Category = category,
            ShortSummary = shortSummary,
            FullReview = fullReview,
            Rating = rating
        };

        return book;
    }

    public void ChangeAuthor(Guid authorId)
    {
        AuthorId = authorId;
    }

    public void ModifyReview(string title, string shortSummary, string fullReview, int? rating)
    {
        Title = title;
        ShortSummary = shortSummary;
        FullReview = fullReview;
        Rating = rating;
    }

    public void ChangeStatus(ReadingStatus status)
    {
        Status = status;
    }

    public void ChangeCategory(BookCategory category)
    {
        Category = category;
    }

    public void Remove()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}
