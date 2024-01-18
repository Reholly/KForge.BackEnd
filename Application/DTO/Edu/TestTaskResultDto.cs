namespace Application.DTO.Edu;

public record TestTaskResultDto
{
    public required double ResultInPercents { get; init; }
    public required int TotalQuestionsCount { get; init; }
    public required int CorrectAnswersCount { get; init; }
}