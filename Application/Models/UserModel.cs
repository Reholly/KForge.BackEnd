namespace Application.Models;

public record UserModel(
    string Name, 
    string Surname, 
    string Patronymic, 
    DateTime BirthDate);
