namespace API.Extensions;

public static class HttpContextExtensions
{
    public static string GetJwtToken(this HttpContext context)
    {
        return context.Request.Headers.Authorization
            .ToString()
            .Split(" ")[1];
    }
}