namespace Application.Exceptions.Auth;

public class ApplicationLayerException(string message, int errorCode) : Exception(message)
{
    public int ErrorCode { get; set; } = errorCode;
}