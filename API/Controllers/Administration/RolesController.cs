using Application.Handlers.Administration.Roles;
using Application.Requests.Administration.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Administration;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/admin/roles")]
public class RolesController : ControllerBase
{
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