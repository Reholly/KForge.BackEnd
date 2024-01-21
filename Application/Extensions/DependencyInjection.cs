using Application.DTO.Auth;
using Application.DTO.Edu;
using Application.Handlers.Admin;
using Application.Handlers.Auth;
using Application.Handlers.Edu.Tasks;
using Application.Handlers.Profile;
using Application.Mappers;
using Application.Mappers.Edu;
using Application.Models;
using Application.Options;
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
        collection.AddScoped<EmailConfirmHandler>();
        
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
        collection.AddScoped<IValidator<LogInDto>, LoginRequestValidator>();
        collection.AddScoped<IValidator<IdentityUserDto>, IdentityModelValidator>();
        collection.AddScoped<IValidator<ApplicationUserDto>, UserModelValidator>();
    }

    public static void AddMappers(this IServiceCollection collection)
    {
        collection.AddScoped<IMapper<TestTask, TestTaskDto>, TestTaskMapper>();
        collection.AddScoped<IMapper<TestTaskResult, TestTaskResultDto>, TestTaskResultMapper>();
    }

    public static void AddApplicationOptions(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddOptions<EmailOptions>().BindConfiguration(EmailOptions.Section);
        collection.AddOptions<JwtOptions>().BindConfiguration(JwtOptions.Section);
        collection.AddOptions<AuthOptions>().BindConfiguration(AuthOptions.Section);
    }
}