using Application.DTO.Edu;
using Application.Models;
using Application.Services.Edu.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services.Edu.Implementations;

public class TestTaskService(
    ITestTaskRepository testTaskRepository,
    ITestTaskResultRepository testTaskResultRepository) : ITestTaskService
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
                        Question = question,
                        IsCorrect = avDto.IsCorrect
                    }).ToList();
                question.AllVariants = allVariants;

                return question;
            })
            .ToList();
        task.Questions = questions;
        await testTaskRepository.CommitChangesAsync(ct);
        return task;
    }

    public async Task<TestTaskResult> PassTestTaskAsync(TestTask task, 
        AnsweredQuestionDto[] answeredQuestions, ApplicationUser user,
        CancellationToken ct = default)
    {
        int correctAnswersCount = 0;
        for (int i = 0; i < answeredQuestions.Length; i++)
        {
            var question = task.Questions!
                .FirstOrDefault(q => q.Text == answeredQuestions[i].Question);
            if (question is null) continue;
            
            var correctAnswer = question.AllVariants!.FirstOrDefault(av => av.IsCorrect);
            if (correctAnswer is null) continue;
            
            if (answeredQuestions[i].Answer == correctAnswer.Text)
            {
                correctAnswersCount++;
            }
        }

        var taskResult = new TestTaskResult
        {
            TotalQuestionsCount = answeredQuestions.Length,
            CorrectAnswersCount = correctAnswersCount,
            ResultInPercents = (double)correctAnswersCount / answeredQuestions.Length * 100.0,
            Student = user,
            StudentId = user.Id,
            TestTask = task,
            TestTaskId = task.Id
        };

        await testTaskResultRepository.SaveAsync(taskResult, ct);
        await testTaskResultRepository.CommitChangesAsync(ct);

        return taskResult;
    }
}