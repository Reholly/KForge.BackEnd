namespace Application.Exceptions.Auth;

public class PasswordResetException(string message) : ApplicationLayerException(message, 400);