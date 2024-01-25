using Application.Handlers.Administration.Users;
using Application.Requests.Administration.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Administration;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/admin/user")]
public class UserManagementController
{
    [HttpPost("/groups/add")]
    public Task AddUserToGroup(
        [FromServices] AddUserToGroupHandler handler,
        [FromBody] AddUserToGroupRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("/groups/remove")]
    public Task RemoveUserFromGroup(
        [FromServices] RemoveUserFromGroupHandler handler,
        [FromBody] RemoveUserFromGroupRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);


    [HttpPost("/ban")]
    public Task BanUserAccount(
        [FromServices] BanUserHandler handler,
        BanUserRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);

    [HttpPost("/unban")]
    public Task UnbanUserAccount(
        [FromServices] UnbanUserHandler handler,
        [FromBody] UnbanUserRequest request,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
}