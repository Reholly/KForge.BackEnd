﻿using System.Security.Claims;
using Application.DTO.Edu;
using Application.Exceptions;
using Application.Exceptions.Auth;
using Application.Mappers;
using Application.Requests.Education.Tasks;
using Application.Services.Auth.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Handlers.Edu.Tasks;

public class UpdateTaskHandler(
    IJwtTokenService jwtTokenService,
    IUserRepository userRepository,
    ITestTaskRepository testTaskRepository,
    IPermissionService permissionService,
    IMapper<TestTask, TestTaskDto> testTaskMapper)
{
    public async Task HandleAsync(UpdateTaskRequest request, string jwtToken, 
        CancellationToken ct = default)
    {
        var tokenClaims = jwtTokenService.ParseClaims(jwtToken);
        var usernameClaim = tokenClaims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
        if (usernameClaim is null)
        {
            throw new PermissionDeniedException("Token doesn't contain username");
        }

        string username = usernameClaim.Value;
        var user = await userRepository.GetByUsernameWithCoursesAsync(username, ct);
        NotFoundException.ThrowIfNull(user, nameof(user));

        var testTask = await testTaskRepository.GetTaskByIdAsync(request.TaskId, ct);
        NotFoundException.ThrowIfNull(testTask, nameof(testTask));

        if (!permissionService.IsCourseMentor(user!, testTask!.CourseId) 
            && tokenClaims.FirstOrDefault(c => 
                c is { Type: ClaimTypes.Role, Value: "Admin" }) is null)
        {
            throw new PermissionDeniedException("Only course mentors " +
                                                "and admins can update tasks");
        }

        testTask = testTaskMapper.MapReverse(testTask, request.TaskDto);
        
        await testTaskRepository.UpdateTaskAsync(testTask, ct);
        await testTaskRepository.CommitChangesAsync(ct);
    }
}