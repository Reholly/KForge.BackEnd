namespace Application.Exceptions.Common;

public class ApplicationException(string message, int errorCode) : Exception(message)
{
    public int ErrorCode { get; set; } = errorCode;
}