namespace Application.Models;

public class ApplicationUserModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string Patronymic { get; set; } = string.Empty;
    public required DateTime BirthDate { get; set; }
}