using Application.DTO.Administration;

namespace Application.Requests.Administration.Users;

public record RemoveUserFromGroupRequest(UserWithGroupDto UserWithGroupDto);