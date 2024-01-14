using Application.Models;
using Application.Services.Auth.Interfaces;
using Application.Services.Edu.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.Edu.Implementations;

public class TestTaskService(ITestTaskRepository testTaskRepository) : ITestTaskService
{
    public async Task<TestTask> CreateTestTaskAsync(CreateTestTaskModel model,
        CancellationToken ct = default)
    {
        var task = new TestTask
        {
            Author = model.Author,
            AuthorId = model.Author.Id,
            Course = model.Course,
            CourseId = model.Course.Id,
            Title = model.TaskDto.Title
        };
        await testTaskRepository.AddTestTaskToDatabaseAsync(task, ct);

        var questions = model.TaskDto.Questions
            .Select(qDto =>
            {
                var question = new Question
                {
                    TestTask = task,
                    TestTaskId = task.Id,
                    Text = qDto.Text
                };

                var allVariants = qDto.AllVariants
                    .Select(avDto => new AnswerVariant
                    {
                        Text = avDto.Text,
                        Question = question
                    }).ToList();
                var correctVariant = new AnswerVariant
                {
                    Text = qDto.CorrectVariant.Text,
                    Question = question
                };
                question.CorrectVariant = correctVariant;
                question.AllVariants = allVariants;

                return question;
            })
            .ToList();
        task.Questions = questions;
        await testTaskRepository.CommitChangesAsync(ct);
        return task;
    }
}