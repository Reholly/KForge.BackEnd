namespace Application.Exceptions.Common;

public class ConflictException(string message) : ApplicationException(message, 409);