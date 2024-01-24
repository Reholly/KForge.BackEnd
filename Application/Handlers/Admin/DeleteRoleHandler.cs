using Application.DTO.Admin;
using Application.Requests.Admin.Roles;
using Application.Services.Admin.Interfaces;

namespace Application.Handlers.Admin;

public class DeleteRoleHandler(IRoleService roleService)
{
    private readonly IRoleService _roleService = roleService;

    public async Task HandleAsync(DeleteRoleRequest request, CancellationToken ct = default)
    {
        await _roleService.DeleteRoleAsync(request.RoleDto.Role);
    }
}