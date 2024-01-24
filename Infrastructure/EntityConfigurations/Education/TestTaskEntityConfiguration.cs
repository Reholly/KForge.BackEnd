using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Education;

public class TestTaskEntityConfiguration : IEntityTypeConfiguration<TestTask>
{
    public void Configure(EntityTypeBuilder<TestTask> builder)
    {
        builder.ToTable("TestTask");

        builder.HasKey(tt => tt.Id);

        builder.HasOne(tt => tt.Course)
            .WithMany(c => c.TestTasks)
            .HasForeignKey(tt => tt.CourseId);

        builder.HasOne(tt => tt.Author)
            .WithMany(au => au.TasksAsAuthor)
            .HasForeignKey(tt => tt.AuthorId);

        builder.HasMany(tt => tt.Questions)
            .WithOne(q => q.TestTask)
            .HasForeignKey(q => q.TestTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(tt => tt.Results)
            .WithOne(ttr => ttr.TestTask)
            .HasForeignKey(ttr => ttr.TestTaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}