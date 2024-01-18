namespace Application.DTO.Edu;

public record AnsweredQuestionDto
{
    public required string Question { get; init; }
    public required string Answer { get; init; }
}