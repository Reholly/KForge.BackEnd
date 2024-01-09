using API.Middlewares;

namespace API.Extensions;

public static class MiddlewareExtensions
{
    public static void UseErrorHandling(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }

    public static void AddErrorHandling(this IServiceCollection collection)
    {
        collection.AddSingleton<ErrorHandlingMiddleware>();
    }
}