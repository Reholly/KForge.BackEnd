using Application.DTO.Edu;
using Application.Handlers.Auth;
using Application.Handlers.Edu.Tasks;
using Application.Handlers.Profile;
using Application.Mappers;
using Application.Mappers.Edu;
using Application.Models;
using Application.Requests.Auth;
using Application.Services.Auth.Implementations;
using Application.Services.Auth.Interfaces;
using Application.Services.Edu.Implementations;
using Application.Services.Edu.Interfaces;
using Application.Validators.Auth;
using Application.Validators.ModelValidators;
using Domain.Entities;
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
        collection.AddScoped<ITestTaskService, TestTaskService>();
    }

    public static void AddHandlers(this IServiceCollection collection)
    {
        collection.AddScoped<LoginHandler>();
        collection.AddScoped<RegisterHandler>();
        collection.AddScoped<RefreshTokenHandler>();
        
        collection.AddScoped<GetProfileHandler>();
        collection.AddScoped<UpdateProfileHandler>();

        collection.AddScoped<GetTaskByIdHandler>();
        collection.AddScoped<CreateTaskHandler>();
    }

    public static void AddValidators(this IServiceCollection collection)
    {
        collection.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        collection.AddScoped<IValidator<IdentityModel>, IdentityModelValidator>();
        collection.AddScoped<IValidator<UserModel>, UserModelValidator>();
    }

    public static void AddMappers(this IServiceCollection collection)
    {
        collection.AddScoped<IMapper<TestTask, TestTaskDto>, TestTaskMapper>();
    }
}