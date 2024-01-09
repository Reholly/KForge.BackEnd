using System.Net.Mime;
using System.Text.Json;
using API.ErrorHandling;
using Application.Exceptions.Auth;
using FluentValidation;

namespace API.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            string errorMessage;
            
            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    errorMessage = JsonSerializer.Serialize(new ServerErrorResponseModel
                    {
                        Errors = validationException.Errors
                            .Select(x => $"{x.ErrorCode} : {x.ErrorMessage}")
                    });
                    break;
                case ApplicationLayerException applicationLayerException:
                    context.Response.StatusCode = applicationLayerException.ErrorCode;
                    errorMessage = JsonSerializer.Serialize(new ServerErrorResponseModel
                    {
                        Errors = new [] { applicationLayerException.Message }
                    });
                    break;
               
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    errorMessage = JsonSerializer.Serialize(new ServerErrorResponseModel
                    {
                        Errors = new [] { exception.Message}
                    });
                    break;
            }
            
            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsync(errorMessage);
        }
    }
}