using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class AnswerVariantEntityConfiguration : IEntityTypeConfiguration<AnswerVariant>
{
    public void Configure(EntityTypeBuilder<AnswerVariant> builder)
    {
        builder.ToTable("AnswerVariant");

        builder.HasKey(av => av.Id);

        builder.HasOne(av => av.Question)
            .WithMany(q => q.AllVariants)
            .HasForeignKey(av => av.QuestionId);
        
        builder.HasOne(av => av.Question)
            .WithOne(q => q.CorrectVariant)
            .HasForeignKey<Question>(q => q.CorrectVariantId);
    }
}