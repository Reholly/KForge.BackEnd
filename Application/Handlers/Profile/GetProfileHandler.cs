using Application.Exceptions.Common;
using Application.Models;
using Application.Requests.Profile;
using Application.Responses.Profile;
using Application.Services.Auth.Interfaces;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Profile;

public class GetProfileHandler(
    IPermissionService permissionService,
    IValidator<GetProfileRequest> validator,
    ApplicationDbContext dbContext)
{
    private readonly IPermissionService _permissionService = permissionService;
    private readonly IValidator<GetProfileRequest> _validator = validator;
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GetProfileResponse> HandleAsync(
        GetProfileRequest request, 
        string jwtToken,
        CancellationToken ct = default)
    {
        await _validator.ValidateAndThrowAsync(request, ct);
        
        bool isOwner = _permissionService.IsProfileOwner(request.Username, jwtToken);
        
        var user = await _dbContext.Profiles
                       .FirstOrDefaultAsync(x => x.Username == request.Username, ct) 
                   ?? throw new NotFoundException("User not found.");

        var userDto = new ApplicationUserDto(user.Name, user.Surname, user.Patronymic, user.BirthDate);

        return new GetProfileResponse(userDto, isOwner);
    }
}