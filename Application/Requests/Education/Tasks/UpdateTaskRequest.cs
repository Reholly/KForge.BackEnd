using Application.DTO.Edu;

namespace Application.Requests.Education.Tasks;

public record UpdateTaskRequest
{
    public required Guid TaskId { get; init; }
    public required TestTaskDto TaskDto { get; init; }
}