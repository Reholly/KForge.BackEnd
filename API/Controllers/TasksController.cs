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
    [HttpGet("{task_id}")]
    public Task GetTaskById(
        [FromRoute, FromQuery] GetTaskByIdRequest request,
        [FromServices] GetTaskByIdHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
}