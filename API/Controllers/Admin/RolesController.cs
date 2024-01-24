using Application.Handlers.Admin;
using Application.Requests.Admin.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/admin/roles")]
public class RolesController : ControllerBase
{
    [HttpPost("/add")]
    public Task CreateRole(
        [FromServices] CreateRoleHandler handler,
        [FromBody] CreateRoleRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("/delete")]
    public Task DeleteRole(
        [FromServices] DeleteRoleHandler handler,
        [FromBody] DeleteRoleRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("/add/mentor")]
    public Task AddMentor( 
        [FromServices] AddMentorHandler handler,
        [FromBody] AddMentorRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("/delete/mentor")]
    public Task RemoveMentor(
        [FromServices] RemoveMentorHandler handler,
        [FromBody] DeleteMentorRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
}