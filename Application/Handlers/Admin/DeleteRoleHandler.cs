using Application.DTO.Admin;
using Application.Services.Admin.Interfaces;

namespace Application.Handlers.Admin;

public class DeleteRoleHandler(IRoleService roleService)
{
    private readonly IRoleService _roleService = roleService;

    public async Task HandleAsync(RoleDto request, CancellationToken ct = default)
    {
        await _roleService.DeleteRoleAsync(request.Role);
    }
}