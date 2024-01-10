using Application.Models;

namespace Application.Responses.Profile;

public record GetProfileResponse(ApplicationUserModel UserModel, bool IsOwner);