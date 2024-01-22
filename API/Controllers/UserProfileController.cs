using API.Extensions;
using Application.Handlers.Profile;
using Application.Requests.Profile;
using Application.Responses.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
[Route("/api/profile")]
public class UserProfileController : ControllerBase
{
    [HttpGet]
    public Task<GetProfileResponse> GetProfile(
        [FromQuery] GetProfileRequest request,
        [FromServices] GetProfileHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, HttpContext.GetJwtToken(),ct);
    
    [HttpPost]
    public Task UpdateProfile(
        [FromBody] UpdateProfileRequest request,
        [FromServices] UpdateProfileHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, HttpContext.GetJwtToken(), ct);
}