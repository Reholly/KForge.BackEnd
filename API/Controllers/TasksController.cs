using API.Extensions;
using Application.Handlers.Edu.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/edu/[controller]")]
public class TasksController : ControllerBase
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("{taskId}")]
    public Task GetTaskById(
        [FromRoute] Guid taskId,
        [FromServices] GetTaskByIdHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(taskId, HttpContext.GetJwtToken(), ct);
}