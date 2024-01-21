namespace Application.Exceptions.Common;

public class PermissionDeniedException(string message) 
    : ApplicationException(message, 403);