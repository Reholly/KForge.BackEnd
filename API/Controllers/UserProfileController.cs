using API.Extensions;
using Application.Handlers.Profile;
using Application.Models;
using Application.Requests.Profile;
using Application.Requests.Wrappers;
using Application.Responses.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("/api/profile")]
public class UserProfileController : ControllerBase
{
    [HttpGet("{username}")]
    public Task<GetProfileResponse> GetProfile(
        [FromRoute] string username,
        [FromServices] GetProfileHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(username, HttpContext.GetJwtToken(),ct);
    
    [HttpPost("{username}")]
    public Task UpdateProfile(
        [FromRoute] string username,
        [FromBody] UpdateProfileRequest request,
        [FromServices] UpdateProfileHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, username, HttpContext.GetJwtToken(), ct);
}