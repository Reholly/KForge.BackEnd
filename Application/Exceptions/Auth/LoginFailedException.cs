namespace Application.Exceptions.Auth;

public class LoginFailedException(string message, int errorCode) : ApplicationLayerException(message, errorCode);