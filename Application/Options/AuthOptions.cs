namespace Application.Options;

public class AuthOptions
{
    public const string Section = "Auth";
    
    public string EmailConfirmationUrl { get; set; } = string.Empty;
}