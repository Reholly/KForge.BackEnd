using API.Extensions;
using Application.DTO.Edu;
using Application.Handlers.Edu.Tasks;
using Application.Requests.Education.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/edu/[controller]")]
public class TasksController : ControllerBase
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("{taskId}")]
    public Task<TestTaskDto> GetTaskById(
        [FromRoute] Guid taskId,
        [FromServices] GetTaskByIdHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(taskId, HttpContext.GetJwtToken(), ct);

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public Task CreateTask(
        [FromBody] CreateTaskRequest request,
        [FromServices] CreateTaskHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, HttpContext.GetJwtToken(), ct);

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut("{taskId}")]
    public Task UpdateTask(
        [FromBody, FromRoute] UpdateTaskRequest request,
        [FromServices] UpdateTaskHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, HttpContext.GetJwtToken(), ct);

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpDelete("{taskId}")]
    public Task DeleteTask(
        [FromRoute] Guid taskId,
        [FromServices] DeleteTaskHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(taskId, HttpContext.GetJwtToken(), ct);
}