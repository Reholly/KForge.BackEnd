namespace Application.DTO.Edu;

public record TestTaskDto
{
    public required Guid Id { get; init; }
    public required QuestionDto[] Questions { get; init; } = Array.Empty<QuestionDto>();
}