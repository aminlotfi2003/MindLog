using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindLog.Domain.Entities;

namespace MindLog.Infrastructure.Persistence.Configurations.Application;

public class SkillItemConfiguration : IEntityTypeConfiguration<SkillItem>
{
    public void Configure(EntityTypeBuilder<SkillItem> builder)
    {
        builder.ToTable("SkillItems", Schemas.Application);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasOne(x => x.Profile)
            .WithMany(y => y.Skills)
            .HasForeignKey(x => x.ProfileId);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
