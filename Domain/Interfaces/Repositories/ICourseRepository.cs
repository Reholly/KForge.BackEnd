using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ICourseRepository
{
    Task<Course?> GetCourseByIdAsync(Guid id, CancellationToken ct = default);
}