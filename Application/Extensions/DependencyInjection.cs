using Application.Handlers.Auth;
using Application.Handlers.Edu.Tasks;
using Application.Handlers.Profile;
using Application.Models;
using Application.Requests.Auth;
using Application.Requests.Education.Tasks;
using Application.Services.Auth.Implementations;
using Application.Services.Auth.Interfaces;
using Application.Validators.Auth;
using Application.Validators.Edu.Tasks;
using Application.Validators.ModelValidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection collection)
    {
        collection.AddScoped<IJwtTokenService, JwtTokenService>();
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddScoped<IPermissionService, PermissionService>();
    }

    public static void AddHandlers(this IServiceCollection collection)
    {
        collection.AddScoped<LoginHandler>();
        collection.AddScoped<RegisterHandler>();
        collection.AddScoped<RefreshTokenHandler>();
        
        collection.AddScoped<GetProfileHandler>();
        collection.AddScoped<UpdateProfileHandler>();

        collection.AddScoped<GetTaskByIdHandler>();
    }

    public static void AddValidators(this IServiceCollection collection)
    {
        collection.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        collection.AddScoped<IValidator<IdentityModel>, IdentityModelValidator>();
        collection.AddScoped<IValidator<UserModel>, UserModelValidator>();
        collection.AddScoped<IValidator<GetTaskByIdRequest>, GetTaskByIdRequestValidator>();
    }
}