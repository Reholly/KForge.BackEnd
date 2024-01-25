using API.Extensions;
using Application.Handlers.Edu.Tasks;
using Application.Requests.Education.Tasks;
using Application.Responses.Education.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Education;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("/api/edu/task")]
public class TaskController : ControllerBase
{
    [HttpGet("{taskId}")]
    public Task<GetTaskByIdResponse> GetTaskById(
        [FromRoute] Guid taskId,
        [FromServices] GetTaskByIdHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(taskId, HttpContext.GetJwtToken(), ct);

    [HttpPost]
    public Task CreateTask(
        [FromBody] CreateTaskRequest request,
        [FromServices] CreateTaskHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, HttpContext.GetJwtToken(), ct);

    [HttpPut("{taskId}")]
    public Task UpdateTask(
        [FromRoute] Guid taskId,
        [FromBody] UpdateTaskRequest request,
        [FromServices] UpdateTaskHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request with { TaskId = taskId }, HttpContext.GetJwtToken(), ct);

    [HttpDelete("{taskId}")]
    public Task DeleteTask(
        [FromRoute] Guid taskId,
        [FromServices] DeleteTaskHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(taskId, HttpContext.GetJwtToken(), ct);

    [HttpPost("{taskId}")]
    public Task<PassTestTaskResponse> PassTestTask(
        [FromRoute] Guid taskId,
        [FromBody] PassTestTaskRequest request,
        [FromServices] PassTestTaskHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request with { TaskId = taskId }, HttpContext.GetJwtToken(), ct);
}