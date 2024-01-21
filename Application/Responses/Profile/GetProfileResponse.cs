using Application.Models;

namespace Application.Responses.Profile;

public record GetProfileResponse(ApplicationUserDto ApplicationUserDto, bool IsOwner);