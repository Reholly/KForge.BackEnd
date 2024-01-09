namespace Application.Exceptions.Auth;


public class RoleAttachingException (string message) : ApplicationLayerException(message, 500);