using Domain.Entities.Base;

namespace Domain.Entities;

public class TestTaskResult : EntityBase
{
    public required Guid StudentId { get; set; }
    public ApplicationUser? Student { get; set; }
    public Guid TestTaskId { get; set; }
    public TestTask? TestTask { get; set; }
    public double ResultInPercents { get; set; }
    public int TotalQuestionsCount { get; set; }
    public int CorrectAnswersCount { get; set; }
}