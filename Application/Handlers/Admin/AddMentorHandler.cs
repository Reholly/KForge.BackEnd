using Application.DTO.Admin;
using Application.Requests.Admin.Roles;
using Application.Services.Admin.Interfaces;

namespace Application.Handlers.Admin;

public class AddMentorHandler(IRoleService roleService)
{
    private readonly string _mentorRole = "Mentor";
    private readonly IRoleService _roleService = roleService;

    public async Task HandleAsync(AddMentorRequest request, CancellationToken ct = default)
    {
        await _roleService.AttachRoleAsync(request.UsernameDto.Username, _mentorRole);
    }
}