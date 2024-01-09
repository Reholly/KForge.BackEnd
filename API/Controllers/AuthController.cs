using Application.Handlers;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/auth/api")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public Task<RegisterResponse> Register(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("login")]
    public Task<LoginResponse> Register(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("refresh")]
    public Task<RefreshTokenResponse> Register(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshTokenHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);

    [HttpGet("test")]
    [Authorize(Roles = "Student", AuthenticationSchemes = "Bearer")]
    public List<string> TestEndpoint()
    {
        return new List<string>{"HAHAL", "HHA", "FSFAF"};
    }
}