namespace Application.Exceptions.Common;

public class BadRequestException(string message) : ApplicationException(message, 400);