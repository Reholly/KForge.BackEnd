using Application.Handlers.Administration.Groups;
using Application.Requests.Administration.Groups;
using Application.Responses.Administration.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Administration;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/admin/groups")]
public class GroupsController
{
    [HttpGet]
    public Task<GetGroupWithUsersResponse> GetGroupWithUsers(
        [FromServices] GetGroupWithUsersHandler handler,
        [FromQuery] GetGroupWithUsersRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPut]
    public Task UpdateGroup(
        [FromServices] UpdateGroupHandler handler,
        [FromBody] UpdateGroupRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpDelete]
    public Task DeleteGroup(
        [FromServices] DeleteGroupHandler handler,
        [FromBody] DeleteGroupRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost]
    public Task CreateGroup(
        [FromServices] CreateGroupHandler handler,
        [FromBody] CreateGroupRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
}