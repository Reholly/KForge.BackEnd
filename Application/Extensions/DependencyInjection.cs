using Application.Configuration.Options;
using Application.Configuration.Setups;
using Application.DTO.Edu;
using Application.Handlers.Admin;
using Application.Handlers.Auth;
using Application.Handlers.Edu.Tasks;
using Application.Handlers.Profile;
using Application.Mappers;
using Application.Mappers.Edu;
using Application.Models;
using Application.Services.Admin.Implementations;
using Application.Services.Admin.Interfaces;
using Application.Services.Auth.Implementations;
using Application.Services.Auth.Interfaces;
using Application.Services.Edu.Implementations;
using Application.Services.Edu.Interfaces;
using Application.Services.Utils.Implementations;
using Application.Services.Utils.Interfaces;
using Application.Validators.Auth;
using Application.Validators.ModelValidators;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LoginRequest = Application.Requests.Auth.LoginRequest;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection collection)
    {
        collection.AddScoped<IJwtTokenService, JwtTokenService>();
        collection.AddScoped<IEmailService, EmailService>();
        collection.AddScoped<IAuthService, AuthService>();
        collection.AddScoped<IPermissionService, PermissionService>();
        collection.AddScoped<ITestTaskService, TestTaskService>();
        collection.AddScoped<IRoleService, RoleService>();
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
        collection.AddScoped<UpdateTaskHandler>();
        collection.AddScoped<DeleteTaskHandler>();
        collection.AddScoped<PassTestTaskHandler>();

        collection.AddScoped<AddMentorHandler>();
        collection.AddScoped<RemoveMentorHandler>();
        collection.AddScoped<CreateRoleHandler>();
        collection.AddScoped<DeleteRoleHandler>();
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
        collection.AddScoped<IMapper<TestTaskResult, TestTaskResultDto>, TestTaskResultMapper>();
    }

    public static void AddOptions(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.ConfigureOptions<JwtOptionsSetup>();
        collection.ConfigureOptions<AuthOptionsSetup>();
        collection.ConfigureOptions<EmailOptionsSetup>();
        
    }
}