using Application.DTO.Edu;

namespace Application.Responses.Education.Tasks;

public record GetTaskByIdResponse
{
    public required TestTaskDto TestTask { get; init; }
}