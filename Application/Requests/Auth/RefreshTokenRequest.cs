namespace Application.Requests.Auth;

public record RefreshTokenRequest(string RefreshToken, string ExpiredAccessToken);