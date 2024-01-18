namespace Application.DTO.Edu;

public record QuestionDto
{
    public required string Text { get; init; }
    public required AnswerVariantDto[] AllVariants { get; init; } = Array.Empty<AnswerVariantDto>();
}