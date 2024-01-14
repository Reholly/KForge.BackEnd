namespace Application.DTO.Edu;

public record AnswerVariantDto
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
}