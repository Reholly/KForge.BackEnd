using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Education;

public class TestTaskResultEntityConfiguration : IEntityTypeConfiguration<TestTaskResult>
{
    public void Configure(EntityTypeBuilder<TestTaskResult> builder)
    {
        builder.ToTable("Result");

        builder.HasKey(ttr => ttr.Id);

        builder.HasOne(ttr => ttr.TestTask)
            .WithMany(tt => tt.Results)
            .HasForeignKey(ttr => ttr.TestTaskId);

        builder.HasOne(ttr => ttr.Student)
            .WithMany(au => au.TestTaskResults)
            .HasForeignKey(ttr => ttr.StudentId);
    }
}