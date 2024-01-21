using Application.DTO.Admin;
using Application.Handlers.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("/api/admin/roles")]
public class RolesController : ControllerBase
{
    [HttpPost("/add")]
    public Task CreateRole(
        [FromServices] CreateRoleHandler handler,
        [FromBody] RoleDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
    
    [HttpPost("/delete")]
    public Task DeleteRole(
        [FromServices] DeleteRoleHandler handler,
        [FromBody] RoleDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
    
    [HttpPost("/add/mentor")]
    public Task AddMentorRole( 
        [FromServices] AddMentorHandler handler,
        [FromBody] UsernameDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
    
    [HttpPost("/delete/mentor")]
    public Task RemoveMentorRole(
        [FromServices] RemoveMentorHandler handler,
        [FromBody] UsernameDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
}