namespace Application.Requests.Auth;

public record LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}