using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class WorkExperienceConfiguration : IEntityTypeConfiguration<WorkExperience>
{
    public void Configure(EntityTypeBuilder<WorkExperience> builder)
    {
        builder.ToTable("WorkExperiences", Schemas.Application);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Company)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.RoleTitle)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.EmploymentType)
            .IsRequired();

        builder.Property(x => x.WorkMode)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasOne(x => x.Profile)
            .WithMany(y => y.WorkExperiences)
            .HasForeignKey(x => x.ProfileId);
    }
}
