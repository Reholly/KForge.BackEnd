using Application.DTO.Edu;
using Application.Models;
using Domain.Entities;

namespace Application.Services.Edu.Interfaces;

public interface ITestTaskService
{
    Task<TestTask> CreateTestTaskAsync(CreateTestTaskModel model, CancellationToken ct = default);
    Task<TestTaskResult> PassTestTaskAsync(TestTask task, AnsweredQuestionDto[] answeredQuestions,
        ApplicationUser user, CancellationToken ct = default);
}