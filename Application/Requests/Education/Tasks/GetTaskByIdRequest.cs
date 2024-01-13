namespace Application.Requests.Education.Tasks;

public record GetTaskByIdRequest
{
    public required Guid TaskId { get; init; }
    public required string Username { get; init; }
}