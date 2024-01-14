namespace Application.DTO.Edu;

public record TestTaskDto
{
    public required string Title { get; init; }
    public required QuestionDto[] Questions { get; init; } = Array.Empty<QuestionDto>();
}