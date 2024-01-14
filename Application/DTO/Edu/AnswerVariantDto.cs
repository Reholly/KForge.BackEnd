namespace Application.DTO.Edu;

public record AnswerVariantDto
{
    public required string Text { get; init; }
    public required bool IsCorrect { get; init; }
}