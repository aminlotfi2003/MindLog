using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class TrainingCourseConfiguration : IEntityTypeConfiguration<TrainingCourse>
{
    public void Configure(EntityTypeBuilder<TrainingCourse> builder)
    {
        builder.ToTable("TrainingCourses", Schemas.Application);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Provider)
            .HasMaxLength(500);

        builder.Property(x => x.CertificateUrl)
            .HasMaxLength(500);

        builder.Property(x => x.CompletionDate)
            .IsRequired(false);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasOne(x => x.Profile)
            .WithMany(y => y.Courses)
            .HasForeignKey(x => x.ProfileId);

        builder.HasIndex(x => x.Title).IsUnique();
    }
}
