namespace Application.Exceptions.Auth;


public class RoleException (string message) : ApplicationLayerException(message, 500);