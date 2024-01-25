using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Education;

public class LectureEntityConfiguration : IEntityTypeConfiguration<Lecture>
{
    public void Configure(EntityTypeBuilder<Lecture> builder)
    {
        builder.ToTable("Lecture");

        builder.HasKey(l => l.Id);

        builder.HasOne(x => x.Course)
            .WithMany(x => x.Lectures)
            .HasForeignKey(x => x.CourseId);

        builder.HasOne(x => x.Section)
            .WithMany(x => x.Lectures)
            .HasForeignKey(x => x.SectionId);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.LecturesAsAuthor)
            .HasForeignKey(x => x.AuthorId);
    }
}