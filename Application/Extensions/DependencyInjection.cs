using Application.DTO.Administration;
using Application.DTO.Auth;
using Application.DTO.Edu;
using Application.DTO.Security;
using Application.Handlers.Administration;
using Application.Handlers.Administration.Departments;
using Application.Handlers.Administration.Groups;
using Application.Handlers.Administration.Roles;
using Application.Handlers.Administration.Users;
using Application.Handlers.Auth;
using Application.Handlers.Edu.Tasks;
using Application.Handlers.Profile;
using Application.Mappers;
using Application.Mappers.Administration;
using Application.Mappers.Edu;
using Application.Models;
using Application.Options;
using Application.Requests.Profile;
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
        collection.AddScoped<IPermissionService, PermissionService>();
        collection.AddScoped<ITestTaskService, TestTaskService>();
        collection.AddScoped<IRoleService, RoleService>();

        collection.AddScoped<ISecurityService, SecurityService>();
        collection.AddScoped<ILogInService, LogInService>();
        collection.AddScoped<IRegistrationService, RegistrationService>();
        collection.AddSingleton<IJwtTokenStorage, JwtTokenStorage>();
    }

    public static void AddHandlers(this IServiceCollection collection)
    {
        //Auth
        collection.AddScoped<LogInHandler>();
        collection.AddScoped<RegisterHandler>();
        collection.AddScoped<RefreshTokenHandler>();
        collection.AddScoped<EmailConfirmHandler>();
        collection.AddScoped<ResetPasswordHandler>();
        
        //Users
        collection.AddScoped<GetProfileHandler>();
        collection.AddScoped<UpdateProfileHandler>();
      
        //Tasks
        collection.AddScoped<GetTaskByIdHandler>();
        collection.AddScoped<CreateTaskHandler>();
        collection.AddScoped<UpdateTaskHandler>();
        collection.AddScoped<DeleteTaskHandler>();
        collection.AddScoped<PassTestTaskHandler>();

        //Administration
        collection.AddScoped<AddMentorHandler>();
        collection.AddScoped<RemoveMentorHandler>();
    
        collection.AddScoped<CreateGroupHandler>();
        collection.AddScoped<DeleteGroupHandler>();
        collection.AddScoped<GetGroupWithUsersHandler>();
        collection.AddScoped<UpdateGroupHandler>();
        
        collection.AddScoped<CreateDepartmentHandler>();
        collection.AddScoped<DeleteDepartmentHandler>();
        collection.AddScoped<GetAllDepartmentsHandler>();
        collection.AddScoped<GetDepartmentByIdHandler>();
        collection.AddScoped<UpdateDepartmentHandler>();

        collection.AddScoped<AddUserToGroupHandler>();
        collection.AddScoped<RemoveUserFromGroupHandler>();
        collection.AddScoped<BanUserHandler>();
        collection.AddScoped<UnbanUserHandler>();
    }

    public static void AddValidators(this IServiceCollection collection)
    {
        collection.AddScoped<IValidator<LogInDto>, LogInDtoValidator>();
        collection.AddScoped<IValidator<IdentityUserDto>, IdentityUserDtoValidator>();
        collection.AddScoped<IValidator<ApplicationUserDto>, ApplicationUserDtoValidator>();
        collection.AddScoped<IValidator<GetProfileRequest>, GetProfileRequestValidator>();
        collection.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordDtoValidator>();
    }

    public static void AddMappers(this IServiceCollection collection)
    {
        collection.AddScoped<IMapper<TestTask, TestTaskDto>, TestTaskMapper>();
        collection.AddScoped<IMapper<TestTaskResult, TestTaskResultDto>, TestTaskResultMapper>();
        collection.AddScoped<IMapper<Group, GroupDto>, GroupToDtoMapper>();
        collection.AddScoped<IMapper<Department, DepartmentDto>, DepartmentToDtoMapper>();
    }

    public static void AddApplicationOptions(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddOptions<EmailOptions>().BindConfiguration(EmailOptions.Section);
        collection.AddOptions<JwtOptions>().BindConfiguration(JwtOptions.Section);
        collection.AddOptions<AuthOptions>().BindConfiguration(AuthOptions.Section);
    }
}