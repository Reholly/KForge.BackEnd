using Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Configuration.Setups;

public class EmailOptionsSetup(IConfiguration configuration) 
    : IConfigureOptions<EmailOptions>
{
    private const string SectionName = "Email";

    private readonly IConfiguration _configuration = configuration;

    public void Configure(EmailOptions optionsSetup)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(optionsSetup);
    }
}