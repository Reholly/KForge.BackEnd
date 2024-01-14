using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TestTaskRepository(ApplicationDbContext context) : ITestTaskRepository
{
    public async Task<TestTask?> GetTaskByIdAsync(Guid taskId, CancellationToken ct = default)
    {
        var task = await context.TestTasks
            .Include(tt => tt.Questions)!
            .ThenInclude(q => q.AllVariants)
            .Include(tt => tt.Questions)
            .FirstOrDefaultAsync(tt => tt.Id == taskId, ct);
        return task;
    }

    public async Task AddTestTaskToDatabaseAsync(TestTask task, CancellationToken ct = default)
        => await context.TestTasks.AddAsync(task, ct);

    public Task CommitChangesAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
}