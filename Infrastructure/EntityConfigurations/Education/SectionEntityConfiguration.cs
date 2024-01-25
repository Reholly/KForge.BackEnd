using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Education;

public class SectionEntityConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Section");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Lectures)
            .WithOne(x => x.Section)
            .HasForeignKey(x => x.SectionId);
        
        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Section)
            .HasForeignKey(x => x.SectionId);

    }
}