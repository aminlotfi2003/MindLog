using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class LanguageProficiencyConfiguration : IEntityTypeConfiguration<LanguageProficiency>
{
    public void Configure(EntityTypeBuilder<LanguageProficiency> builder)
    {
        builder.ToTable("LanguageProficiencies", Schemas.Application);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Language)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.Certificate)
            .HasMaxLength(200);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasOne(x => x.Profile)
            .WithMany(y => y.Languages)
            .HasForeignKey(x => x.ProfileId);

        builder.HasIndex(x => x.Language).IsUnique();
    }
}
