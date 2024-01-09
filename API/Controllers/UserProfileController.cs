using Application.Handlers.Profile;
using Application.Requests.Profile;
using Application.Requests.Wrappers;
using Application.Responses.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/profile")]
public class UserProfileController : ControllerBase
{
    [HttpGet("{username}")]
    [Authorize(Roles = "Student")]
    public Task<GetProfileResponse> GetProfile(
        string username,
        [FromServices] GetProfileHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(new AuthorizationWrapperRequest<GetProfileRequest>(
                HttpContext.Request.Headers.Authorization!, 
                new GetProfileRequest(username)),
            ct);
    
    //ChangeData()
    //
}