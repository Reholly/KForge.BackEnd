using Application.Requests.Administration.Roles;
using Application.Services.Admin.Interfaces;

namespace Application.Handlers.Administration.Roles;

public class AddMentorHandler(IRoleService roleService)
{
    private readonly string _mentorRole = "Mentor";
    private readonly IRoleService _roleService = roleService;

    public async Task HandleAsync(AddMentorRequest request, CancellationToken ct = default)
    {
        await _roleService.AttachRoleAsync(request.Username, _mentorRole);
        //прикрепить к курсу
    }
}