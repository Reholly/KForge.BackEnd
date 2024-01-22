using Application.Exceptions.Common;
using Application.Options;
using Application.Services.Auth.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Application.Services.Auth.Implementations;

public class JwtTokenStorage(
    IMemoryCache memoryCache, 
    IOptions<JwtOptions> options) 
    : IJwtTokenStorage
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly JwtOptions _options = options.Value;
    
    public void Set(string username, string token)
    {
        _memoryCache.Set(
            token, 
            username, 
            new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_options.RefreshTokenExpiresInSeconds)));
    }

    public void Remove(string token)
    {
        _memoryCache.Remove(token);
    }

    public void TryGetValue(string token, out string? username)
    {
        _memoryCache.TryGetValue(token, out string? value);

        username = value ?? throw new NotFoundException("Token not found.");
    }
}