using Application.Models;

namespace Application.Requests.Profile;

public record UpdateProfileRequest(
    string Username, 
    ApplicationUserModel ApplicationUserModel);