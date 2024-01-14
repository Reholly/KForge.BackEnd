using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class QuestionEntityConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Question");

        builder.HasKey(q => q.Id);

        builder.HasMany(q => q.AllVariants)
            .WithOne(av => av.Question)
            .HasForeignKey(av => av.QuestionId);
        
        builder.HasOne(q => q.TestTask)
            .WithMany(tt => tt.Questions)
            .HasForeignKey(q => q.TestTaskId);
    }
}