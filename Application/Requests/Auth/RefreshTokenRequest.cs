namespace Application.Requests.Auth;

public record RefreshTokenRequest
{
    public required string RefreshToken { get; set; }
    public required string ExpiredAccessToken { get; set; }
}