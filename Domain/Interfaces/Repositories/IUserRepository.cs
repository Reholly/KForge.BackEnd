using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByEmailAsync(string email);
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task<ApplicationUser> UpdateUser(ApplicationUser user);
    Task<ApplicationUser> DeleteUser(ApplicationUser user);
    Task<ApplicationUser> DeleteUserByEmailAsync(string email);
}