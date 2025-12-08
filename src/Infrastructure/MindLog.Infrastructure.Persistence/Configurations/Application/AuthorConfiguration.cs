using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors", Schemas.Application);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(x => x.DeletedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(x => x.Books)
            .WithOne(h => h.Author)
            .HasForeignKey(h => h.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
