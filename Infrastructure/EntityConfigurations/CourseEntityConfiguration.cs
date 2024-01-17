using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(c => c.Id);
        builder.HasAlternateKey(c => c.Title);

        builder.HasMany(c => c.Mentors)
            .WithMany(au => au.CoursesAsMentor);

        builder.HasMany(c => c.Students)
            .WithMany(au => au.CoursesAsStudent);

        builder.HasMany(c => c.TestTasks)
            .WithOne(tt => tt.Course)
            .HasForeignKey(tt => tt.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}