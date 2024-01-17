using Application.DTO.Edu;
using Domain.Entities;

namespace Application.Mappers.Edu;

public class TestTaskMapper : IMapper<TestTask, TestTaskDto>
{
    public TestTaskDto Map(TestTask from)
        => new()
        {
            Title = from.Title,
            Questions = from.Questions!
                .Select(q => new QuestionDto
                {
                    Text = q.Text,
                    AllVariants = q.AllVariants!.Select(av => new AnswerVariantDto
                    {
                        Text = av.Text,
                        IsCorrect = av.IsCorrect
                    }).ToArray()
                }).ToArray()
        };

    public TestTask MapReverse(TestTask dest, TestTaskDto src)
    {
        dest.Title = src.Title;
        dest.Questions = src.Questions.Select(qDto =>
            {
                var question = new Question
                {
                    TestTask = dest,
                    TestTaskId = dest.Id,
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
        return dest;
    }
}