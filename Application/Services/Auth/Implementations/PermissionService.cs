using System.Security.Claims;
using Application.Services.Auth.Interfaces;

namespace Application.Services.Auth.Implementations;

public class PermissionService : IPermissionService
{
    public bool IsProfileOwner(string profileOwnerUsername, Claim[] tokenClaims) 
        => tokenClaims.FirstOrDefault(x => x.Value == profileOwnerUsername) is not null;
    
    public bool IsCourseMentor(string[] courseMentors, Claim[] tokenClaims)
    {
        throw new NotImplementedException();
    }
}