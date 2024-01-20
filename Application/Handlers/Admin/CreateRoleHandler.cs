using Application.DTO.Admin;
using Application.Services.Admin.Interfaces;

namespace Application.Handlers.Admin;

public class CreateRoleHandler(IRoleService roleService)
{
    private readonly IRoleService _roleService = roleService;

    public async Task HandleAsync(RoleDto dto, CancellationToken ct = default)
    {
        await _roleService.CreateRoleAsync(dto.Role);
    }
}