namespace Application.Requests.Auth;

public record RefreshTokenDto(string RefreshToken, string ExpiredAccessToken);