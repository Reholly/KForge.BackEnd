namespace Application.Exceptions.Auth;

public class PermissionDeniedException(string message) : ApplicationLayerException(message, 401);