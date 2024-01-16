using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class TestTaskResultRepository(ApplicationDbContext context) : ITestTaskResultRepository
{
    public async Task SaveAsync(TestTaskResult testTaskResult, CancellationToken ct = default)
        => await context.Results.AddAsync(testTaskResult, ct);

    public Task CommitChangesAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
}