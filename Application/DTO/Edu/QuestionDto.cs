namespace Application.DTO.Edu;

public record QuestionDto
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
    public required AnswerVariantDto[] AllVariants { get; init; } = Array.Empty<AnswerVariantDto>();
    public required AnswerVariantDto CorrectVariant { get; init; }
}