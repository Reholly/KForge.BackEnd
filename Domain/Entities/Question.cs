using Domain.Entities.Base;

namespace Domain.Entities;

public class Question : EntityBase
{
    public required string Text { get; set; }
    public Guid TestTaskId { get; set; }
    public TestTask? TestTask { get; set; }
    public ICollection<AnswerVariant>? AllVariants { get; set; }
    public Guid CorrectVariantId { get; set; }
    public AnswerVariant? CorrectVariant { get; set; } 
}