using API.Extensions;
using Application.DTO.Auth;
using Application.Handlers.Auth;
using Application.Requests.Auth;
using Application.Responses.Auth;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Application.Requests.Auth.RegisterRequest;
using ResetPasswordRequest = Application.Requests.Auth.ResetPasswordRequest;

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
        [FromBody] LogInRequest request,
        [FromServices] LogInHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("refresh")]
    public Task<RefreshTokenResponse> Refresh(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshTokenHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpGet("confirm")]
    public Task ConfirmEmail(
        [FromQuery] ConfirmEmailDto dto, 
        [FromServices] EmailConfirmHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);
    
    [HttpPost("reset")]
    public Task ResetPassword(
        [FromBody] ResetPasswordRequest request,
        [FromServices] ResetPasswordHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, HttpContext.GetJwtToken(), ct);
}