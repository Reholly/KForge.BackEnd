using Application.Handlers.Auth;
using Application.Handlers.Profile;
using Application.Models;
using Application.Services.Auth.Implementations;
using Application.Services.Auth.Interfaces;
using Application.Services.Utils.Implementations;
using Application.Services.Utils.Interfaces;
using Application.Validators.Auth;
using Application.Validators.ModelValidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using LoginRequest = Application.Requests.Auth.LoginRequest;
using ResetPasswordRequest = Application.Requests.Auth.ResetPasswordRequest;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection collection)
    {
        collection.AddScoped<IJwtTokenService, JwtTokenService>();
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddScoped<IPermissionService, PermissionService>();
        collection.AddScoped<IEmailService, EmailService>();
    }

    public static void AddHandlers(this IServiceCollection collection)
    {
        collection.AddScoped<LoginHandler>();
        collection.AddScoped<RegisterHandler>();
        collection.AddScoped<RefreshTokenHandler>();
        collection.AddScoped<EmailConfirmHandler>();
        
        collection.AddScoped<GetProfileHandler>();
        collection.AddScoped<UpdateProfileHandler>();
        collection.AddScoped<ResetPasswordHandler>();
    }

    public static void AddValidators(this IServiceCollection collection)
    {
        collection.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        collection.AddScoped<IValidator<IdentityModel>, IdentityModelValidator>();
        collection.AddScoped<IValidator<UserModel>, UserModelValidator>();
        collection.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordRequestValidator>();
    }
}