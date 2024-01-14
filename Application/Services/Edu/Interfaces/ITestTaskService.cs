using Application.Models;
using Domain.Entities;

namespace Application.Services.Edu.Interfaces;

public interface ITestTaskService
{
    Task<TestTask> CreateTestTaskAsync(CreateTestTaskModel model, CancellationToken ct = default);
}