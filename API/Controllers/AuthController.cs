using Application.Handlers.Auth;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public Task Register(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("login")]
    public Task<LoginResponse> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("refresh")]
    public Task<RefreshTokenResponse> Refresh(
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

    [HttpGet("confirm")]
    public Task ConfirmEmail(
        [FromQuery] string username,
        [FromQuery] string code,
        [FromServices] EmailConfirmHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(username, code, ct);
}