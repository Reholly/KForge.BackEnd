using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CourseRepository(ApplicationDbContext context) : ICourseRepository
{
    public Task<Course?> GetCourseByIdAsync(Guid id, CancellationToken ct = default)
        => context.Courses.FirstOrDefaultAsync(c => c.Id == id, ct);
}