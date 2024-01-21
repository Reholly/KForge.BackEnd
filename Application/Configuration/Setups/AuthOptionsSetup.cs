using Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Configuration.Setups;

public class AuthOptionsSetup(IConfiguration configuration)
    : IConfigureOptions<AuthOptions>
{
    private const string SectionName = "Auth";
    
    private readonly IConfiguration _configuration = configuration;
    
    public void Configure(AuthOptions options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}