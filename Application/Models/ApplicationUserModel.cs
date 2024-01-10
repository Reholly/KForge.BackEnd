namespace Application.Models;

public record ApplicationUserModel(
    string Name, 
    string Surname, 
    string Patronymic, 
    DateTime BirthDate);
