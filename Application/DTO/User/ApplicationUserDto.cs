namespace Application.Models;

public record ApplicationUserDto(
    string Name, 
    string Surname, 
    string Patronymic, 
    DateTime BirthDate);
