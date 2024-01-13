using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) 
    : IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateAsync(ApplicationUser user, CancellationToken ct = default)
    {
        await _context.AddAsync(user, ct);
    }
    public async Task<ApplicationUser> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        var user = await _context.Profiles.FirstOrDefaultAsync(x => x.Username == username, ct);

        if (user is null)
            throw new InvalidOperationException("No user with such username.");

        return user;
    }

    public async Task<ApplicationUser> GetByUsernameWithCoursesAsync(string username, CancellationToken ct = default)
    {
        var user = await _context.Profiles
            .Include(au => au.CoursesAsMentor)
            .Include(au => au.TasksAsAuthor)
            .FirstOrDefaultAsync(x => x.Username == username, ct);

        if (user is null)
            throw new InvalidOperationException("No user with such username.");

        return user;
    }

    public async Task UpdateUserAsync(ApplicationUser user, CancellationToken ct = default)
    {
        await Task.Run(() =>_context.Profiles.Update(user), ct);
    }

    public Task DeleteUserAsync(ApplicationUser user, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserByEmailAsync(string email, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CommitAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}