namespace Application.DTO.Auth;

public record IdentityUserDto(
    string Username, 
    string Email, 
    string Password);