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
            .Include(tt => tt.Questions)!
            .ThenInclude(q => q.CorrectVariant)
            .FirstOrDefaultAsync(tt => tt.Id == taskId, ct);
        return task;
    }
}