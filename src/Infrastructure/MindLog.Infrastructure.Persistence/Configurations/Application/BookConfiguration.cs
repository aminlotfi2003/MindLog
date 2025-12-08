using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books", Schemas.Application);

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

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CoverImagePath)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.Category)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.ShortSummary)
            .IsRequired();

        builder.Property(x => x.FullReview)
            .IsRequired();

        builder.Property(x => x.Rating)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(x => x.Author)
            .WithMany(u => u.Books)
            .HasForeignKey(x => x.AuthorId);
    }
}
