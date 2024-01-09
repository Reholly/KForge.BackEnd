namespace Application.Exceptions.Auth;

public class LoginFailedException(string message) : ApplicationLayerException(message, 401);