using Application.DTO.Admin;
using Application.Requests.Admin.Roles;
using Application.Services.Admin.Interfaces;

namespace Application.Handlers.Admin;

public class CreateRoleHandler(IRoleService roleService)
{
    private readonly IRoleService _roleService = roleService;

    public async Task HandleAsync(CreateRoleRequest request, CancellationToken ct = default)
    {
        await _roleService.CreateRoleAsync(request.RoleDto.Role);
    }
}