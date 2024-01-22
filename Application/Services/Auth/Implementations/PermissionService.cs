using Application.Services.Auth.Interfaces;
using Domain.Entities;

namespace Application.Services.Auth.Implementations;

public class PermissionService(IJwtTokenService tokenService)
    : IPermissionService
{
    private readonly IJwtTokenService _tokenService = tokenService;
    
    public bool IsProfileOwner(string profileOwnerUsername, string jwtToken)
    {
        var claims = _tokenService.ParseClaims(jwtToken);
        
        return claims.FirstOrDefault(x => x.Value == profileOwnerUsername) is not null;
    }

    public bool IsCourseMentor(ApplicationUser user, Guid courseId)
        => user.CoursesAsMentor.FirstOrDefault(c => c.Id == courseId) is not null;

    public bool IsInCourse(ApplicationUser user, Guid courseId)
        => user.CoursesAsMentor.FirstOrDefault(c => c.Id == courseId) is not null
           || user.CoursesAsStudent.FirstOrDefault(c => c.Id == courseId) is not null;
}