using Application.Requests.Administration.Roles;
using Application.Services.Admin.Interfaces;

namespace Application.Handlers.Administration.Roles;

public class RemoveMentorHandler(IRoleService roleService)
{
    private readonly string _mentorRole = "Mentor";
    private readonly IRoleService _roleService = roleService;

    public async Task HandleAsync(DeleteMentorRequest request, CancellationToken ct = default)
    {
        await _roleService.DetachRoleAsync(request.UsernameDto.Username, _mentorRole);
    }
}