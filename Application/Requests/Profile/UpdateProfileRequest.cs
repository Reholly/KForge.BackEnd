using Application.Models;

namespace Application.Requests.Profile;

public record UpdateProfileRequest(string ProfileUsername,ApplicationUserDto ApplicationUserDto);