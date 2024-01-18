using Application.DTO.Edu;

namespace Application.Requests.Education.Tasks;

public record PassTestTaskRequest
{
    public required Guid TaskId { get; init; }
    public required AnsweredQuestionDto[] AnsweredQuestions { get; init; } 
        = Array.Empty<AnsweredQuestionDto>();
}