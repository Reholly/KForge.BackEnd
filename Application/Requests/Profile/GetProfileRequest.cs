using System.Security.Claims;

namespace Application.Requests.Profile;

public record GetProfileRequest(string Username);