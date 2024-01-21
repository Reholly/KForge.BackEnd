namespace Application.Configuration.Options;

public class EmailOptions
{
    public string KForgeEmail { get; init; } = string.Empty;
    public string Host { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public int Port { get; init; }
}