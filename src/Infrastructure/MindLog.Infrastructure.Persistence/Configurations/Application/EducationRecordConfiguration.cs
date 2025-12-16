using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class EducationRecordConfiguration : IEntityTypeConfiguration<EducationRecord>
{
    public void Configure(EntityTypeBuilder<EducationRecord> builder)
    {
        builder.ToTable("EducationRecords", Schemas.Application);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Institution)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Degree)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.FieldOfStudy)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired(false);

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasOne(x => x.Profile)
            .WithMany(y => y.Educations)
            .HasForeignKey(x => x.ProfileId);
    }
}
