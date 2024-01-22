using Infrastructure.Contexts;
using Infrastructure.Contexts.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        collection.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var updateAuditableEntitiesInterceptor =
                serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(updateAuditableEntitiesInterceptor);
        });
    }
}