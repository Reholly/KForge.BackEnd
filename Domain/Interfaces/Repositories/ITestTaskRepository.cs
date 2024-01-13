using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITestTaskRepository
{
    Task<TestTask?> GetTaskByIdAsync(Guid taskId, CancellationToken ct = default);
}