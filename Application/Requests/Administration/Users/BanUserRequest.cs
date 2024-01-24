using Application.DTO.Administration;

namespace Application.Requests.Administration.Users;

public record BanUserRequest(
    UsernameDto UsernameDto, 
    string Reason, int Days, 
    bool IsPermanent);