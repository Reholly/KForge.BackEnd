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
}