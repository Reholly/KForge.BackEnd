namespace Application.Exceptions.Auth;

public class JwtTokenRefreshException(string message) : ApplicationLayerException(message, 400);