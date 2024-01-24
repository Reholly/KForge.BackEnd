using Application.Handlers.Administration.Users;
using Application.Requests.Administration.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Administration;

[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/admin/users")]
public class UsersAdministrationController
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


    [HttpPost]
    public async Task BanUserAccount()
    {
        
    }
    
    [HttpPost]
    public async Task UnbanUserAccount()
    {
        
    }
}