namespace Application.Responses.Profile;

public record GetProfileResponse(string Name, string Surname, string Patronymic, string Username);