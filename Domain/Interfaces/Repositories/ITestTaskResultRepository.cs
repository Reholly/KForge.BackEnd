using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ITestTaskResultRepository
{
    Task SaveAsync(TestTaskResult testTaskResult, CancellationToken ct = default);
    Task CommitChangesAsync(CancellationToken ct = default);
}