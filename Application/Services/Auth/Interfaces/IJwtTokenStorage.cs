namespace Application.Services.Auth.Interfaces;

public interface IJwtTokenStorage
{
    void Set(string username, string token);
    void Remove(string token);
    void TryGetValue(string token, out string? username);
    bool IsInBlackList(string username);
    void AddToBlackList(string username, int days);
    void RemoveFromBlackList(string username);
}