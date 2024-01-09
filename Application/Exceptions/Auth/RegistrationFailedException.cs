namespace Application.Exceptions.Auth;

public class RegistrationFailedException (string message) : ApplicationLayerException(message, 400);