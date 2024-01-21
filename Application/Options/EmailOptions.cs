namespace Application.Options;

public class EmailOptions
{
    public const string Section = "Email";
    
    public string KForgeEmail { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Port { get; set; }

    public string From { get; set; } = string.Empty;
}