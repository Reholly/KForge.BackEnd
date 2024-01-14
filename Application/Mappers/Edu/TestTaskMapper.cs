using Application.DTO.Edu;
using Domain.Entities;

namespace Application.Mappers.Edu;

public class TestTaskMapper : IMapper<TestTask, TestTaskDto>
{
    public TestTaskDto Map(TestTask from)
        => new()
        {
            Id = from.Id,
            Questions = from.Questions!
                .Select(q => new QuestionDto
                {
                    Id = q.Id, 
                    Text = q.Text,
                    AllVariants = q.AllVariants!.Select(av => new AnswerVariantDto
                    {
                        Id = av.Id,
                        Text = av.Text
                    }).ToArray(),
                    CorrectVariant = new AnswerVariantDto
                    {
                        Id = q.CorrectVariant!.Id,
                        Text = q.CorrectVariant!.Text
                    }
                }).ToArray()
        };
}