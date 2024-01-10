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
        [FromServices] GetProfileHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(
            new AuthorizationWrapperRequest<GetProfileRequest>(
                HttpContext.Request.Headers.Authorization!, 
                new GetProfileRequest()), 
            new RequestParametersModel(HttpContext.Request.RouteValues),
            ct);
    
    [HttpPost("{username}")]
    public Task<UpdateProfileResponse> UpdateProfile(
        [FromBody] UpdateProfileRequest request,
        [FromServices] UpdateProfileHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(new AuthorizationWrapperRequest<UpdateProfileRequest>(
                HttpContext.Request.Headers.Authorization!, 
                request),
            new RequestParametersModel(HttpContext.Request.RouteValues),
            ct);
}