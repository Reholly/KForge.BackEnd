namespace Application.Requests.Auth;

public record RegisterRequest
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string Patronymic { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}