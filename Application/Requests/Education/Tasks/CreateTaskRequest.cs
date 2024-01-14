using Application.DTO.Edu;

namespace Application.Requests.Education.Tasks;

public record CreateTaskRequest
{
    public required Guid CourseId { get; init; }
    public required TestTaskDto TaskDto { get; init; }
}