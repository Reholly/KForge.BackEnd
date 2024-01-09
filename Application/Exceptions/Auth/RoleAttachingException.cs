namespace Application.Exceptions.Auth;


public class RoleAttachingException (string message, int errorCode) : ApplicationLayerException(message, errorCode);