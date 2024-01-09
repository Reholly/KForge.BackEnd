using Application.Models;

namespace Application.Requests.Auth;

public record RegisterRequest
{
    public required ApplicationUserModel ApplicationUserModel { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}