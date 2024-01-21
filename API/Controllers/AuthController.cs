using API.Extensions;
using Application.DTO.Auth;
using Application.DTO.Security;
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
        [FromBody] LogInDto request,
        [FromServices] LoginHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(request, ct);
    
    [HttpPost("refresh")]
    public Task<RefreshTokenResponse> Refresh(
        [FromBody] RefreshTokenDto dto,
        [FromServices] RefreshTokenHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, ct);

    [HttpGet("test")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public List<string> TestEndpoint()
    {
        return new List<string>{"HAHAL", "HHA", "FSFAF"};
    }

    [HttpGet("confirm")]
    public Task ConfirmEmail(
        [FromQuery, FromRoute] ConfirmEmailDto dto, 
        [FromServices] EmailConfirmHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(dto.Username, dto.Code, ct);
    
    [HttpPost("reset")]
    public Task ResetPassword(
        [FromBody] ResetPasswordDto dto,
        [FromServices] ResetPasswordHandler handler,
        CancellationToken ct = default)
        => handler.HandleAsync(dto, HttpContext.GetJwtToken(), ct);
}