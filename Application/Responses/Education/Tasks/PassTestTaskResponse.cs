using Application.DTO.Edu;

namespace Application.Responses.Education.Tasks;

public record PassTestTaskResponse
{
    public required TestTaskResultDto Result { get; init; }
}