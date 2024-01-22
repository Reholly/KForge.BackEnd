namespace Application.Services.Auth.Interfaces;

public interface IJwtTokenStorage
{
    void Set(string username, string token);
    void Remove(string token);
    void TryGetValue(string token, out string? username);
}