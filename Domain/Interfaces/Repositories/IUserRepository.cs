using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task CreateAsync(ApplicationUser user, CancellationToken ct = default);
    Task<ApplicationUser> GetByUsernameAsync(string username,CancellationToken ct = default);
    Task<ApplicationUser?> GetByUsernameWithCoursesAsync(string username, CancellationToken ct = default);
    Task UpdateUserAsync(ApplicationUser user, CancellationToken ct = default);
    Task DeleteUserAsync(ApplicationUser user, CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
}