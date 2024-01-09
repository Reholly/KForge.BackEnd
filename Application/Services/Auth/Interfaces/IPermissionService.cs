using System.Security.Claims;

namespace Application.Services.Auth.Interfaces;

public interface IPermissionService
{ 
    bool IsProfileOwner(string profileOwnerUsername, Claim[] tokenClaims);
    bool IsCourseMentor(string[] courseMentors, Claim[] tokenClaims);
}