using Application.DTO.Admin;
using Application.Handlers.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("/api/admin")]
public class AdminController : ControllerBase
{
    [HttpPost("/roles/create")]
    public Task CreateRole(
        [FromServices] CreateRoleHandler handler,
        [FromBody] RoleDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
    
    [HttpPost("/roles/delete")]
    public Task DeleteRole(
        [FromServices] DeleteRoleHandler handler,
        RoleDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
    
    [HttpPost("/mentor/add")]
    public Task AddMentorRole( 
        [FromServices] AddMentorHandler handler,
        UsernameDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
    
    [HttpPost("/mentor/remove")]
    public Task RemoveMentorRole(
        [FromServices] RemoveMentorHandler handler,
        UsernameDto dto,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
}