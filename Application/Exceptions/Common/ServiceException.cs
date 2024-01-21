namespace Application.Exceptions.Common;

public class ServiceException(string message) 
    : ApplicationException(message, 500);