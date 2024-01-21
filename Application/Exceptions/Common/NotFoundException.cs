namespace Application.Exceptions.Common;

public class NotFoundException(string message) : ApplicationException(message, 404)
{
    public static void ThrowIfNull(object? o, string argumentName)
    {
        if (o is null)
        {
            throw new NotFoundException($"{argumentName} wasn't found (null)");
        }
    }
}