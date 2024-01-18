namespace Application.Exceptions;

public class NotFoundException(string message) : ApplicationLayerException(message, 404)
{
    public static void ThrowIfNull(object? o, string argumentName)
    {
        if (o is null)
        {
            throw new NotFoundException($"{argumentName} wasn't found (null)");
        }
    }
}