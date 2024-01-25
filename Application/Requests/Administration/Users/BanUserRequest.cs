using Application.DTO.Administration;

namespace Application.Requests.Administration.Users;

public record BanUserRequest(
    string Username, 
    string Reason, 
    int Days);