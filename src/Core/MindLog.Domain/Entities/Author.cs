using MindLog.SharedKernel.Abstractions;

namespace MindLog.Domain.Entities;

public class Author : IEntity<Guid>, IAuditableEntity, ISoftDeletable
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public ICollection<Book> Books { get; set; } = new HashSet<Book>();

    public Author() { } // for EF

    public static Author Create(string firstName, string lastName)
    {
        var author = new Author
        {
            FirstName = firstName,
            LastName = lastName
        };

        return author;
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
