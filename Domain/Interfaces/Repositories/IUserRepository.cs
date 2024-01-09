using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task CreateAsync(ApplicationUser user, CancellationToken ct = default);
    Task<ApplicationUser> GetByEmailAsync(string username,CancellationToken ct = default);
    Task UpdateUser(ApplicationUser user, CancellationToken ct = default);
    Task DeleteUser(ApplicationUser user, CancellationToken ct = default);
    Task DeleteUserByEmailAsync(string email, CancellationToken ct = default);
    Task CommitAsync();
}