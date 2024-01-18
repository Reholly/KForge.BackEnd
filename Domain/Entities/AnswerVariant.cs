using Domain.Entities.Base;

namespace Domain.Entities;

public class AnswerVariant : EntityBase
{
    public required string Text { get; set; }
    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
    public bool IsCorrect { get; set; }
}