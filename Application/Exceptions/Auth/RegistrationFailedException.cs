namespace Application.Exceptions.Auth;

public class RegistrationFailedException (string message, int errorCode) : ApplicationLayerException(message, errorCode);