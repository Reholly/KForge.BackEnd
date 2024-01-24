using Application.DTO.Edu;
using Application.Models;
using Application.Services.Edu.Interfaces;
using Domain.Entities;
using Infrastructure.Contexts;

namespace Application.Services.Edu.Implementations;

public class TestTaskService(ApplicationDbContext context) : ITestTaskService
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
        await context.TestTasks.AddAsync(task, ct);

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
        await context.SaveChangesAsync(ct);
        return task;
    }

    public async Task<TestTaskResult> PassTestTaskAsync(TestTask task, 
        AnsweredQuestionDto[] answeredQuestions, ApplicationUser user,
        CancellationToken ct = default)
    {
        int correctAnswersCount = 0;
        foreach (var answeredQuestion in answeredQuestions)
        {
            var question = task.Questions!
                .FirstOrDefault(q => q.Text == answeredQuestion.Question);
            if (question is null) continue;
            
            var correctAnswer = question.AllVariants!.FirstOrDefault(av => av.IsCorrect);
            if (correctAnswer is null) continue;
            
            if (answeredQuestion.Answer == correctAnswer.Text)
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

        await context.Results.AddAsync(taskResult, ct);
        await context.SaveChangesAsync(ct);

        return taskResult;
    }
}