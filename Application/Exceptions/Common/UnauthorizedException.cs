namespace Application.Exceptions.Common;

public class UnauthorizedException(string message)
    : ApplicationException(message, 401);
