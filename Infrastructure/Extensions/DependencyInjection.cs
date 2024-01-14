using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddRepositories(this IServiceCollection collection)
    {
        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<ITestTaskRepository, TestTaskRepository>();
    }
}