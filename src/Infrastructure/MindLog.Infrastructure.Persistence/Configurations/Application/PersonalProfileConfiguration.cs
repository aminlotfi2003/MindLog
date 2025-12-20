using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class PersonalProfileConfiguration : IEntityTypeConfiguration<PersonalProfile>
{
    public void Configure(EntityTypeBuilder<PersonalProfile> builder)
    {
        builder.ToTable("PersonalProfiles", Schemas.Application);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.FullName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Summary)
            .IsRequired(false);

        builder.Property(x => x.BirthDate)
            .IsRequired(false);

        builder.Property(y => y.Email)
                .HasColumnName("Email")
                .HasMaxLength(500);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(y => y.PhoneNumber)
            .HasColumnName("PhoneNumber")
            .HasMaxLength(50);

        builder.HasIndex(x => x.PhoneNumber).IsUnique();

        builder.Property(y => y.Website)
            .HasColumnName("Website")
            .HasMaxLength(500);

        builder.HasIndex(x => x.Website).IsUnique();

        builder.Property(y => y.LinkedInUrl)
            .HasColumnName("LinkedInUrl")
            .HasMaxLength(500);

        builder.HasIndex(x => x.LinkedInUrl).IsUnique();

        builder.Property(y => y.GitHubUrl)
            .HasColumnName("GitHubUrl")
            .HasMaxLength(500);

        builder.HasIndex(x => x.GitHubUrl).IsUnique();

        builder.Property(y => y.Address)
            .HasMaxLength(100);

        builder.OwnsOne(x => x.Image, cfg =>
        {
            cfg.Property(y => y.FileName)
                .HasColumnName("FileName")
                .IsRequired();

            cfg.Property(y => y.ContentType)
                .HasColumnName("ContentType")
                .IsRequired();

            cfg.Property(y => y.SizeBytes)
                .HasColumnName("SizeBytes")
                .IsRequired();

            cfg.Property(y => y.UploadedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()");
        });

        builder.HasMany(x => x.WorkExperiences)
            .WithOne(y => y.Profile)
            .HasForeignKey(y => y.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Educations)
            .WithOne(y => y.Profile)
            .HasForeignKey(y => y.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Languages)
            .WithOne(y => y.Profile)
            .HasForeignKey(y => y.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Skills)
            .WithOne(y => y.Profile)
            .HasForeignKey(y => y.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Courses)
            .WithOne(y => y.Profile)
            .HasForeignKey(y => y.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.FullName).IsUnique();
    }
}
