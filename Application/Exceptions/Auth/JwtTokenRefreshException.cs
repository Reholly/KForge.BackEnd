namespace Application.Exceptions.Auth;

public class JwtTokenRefreshException(string message, int errorCode) : ApplicationLayerException(message, errorCode);