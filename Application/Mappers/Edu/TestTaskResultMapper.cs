using Application.DTO.Edu;
using Domain.Entities;

namespace Application.Mappers.Edu;

public class TestTaskResultMapper : IMapper<TestTaskResult, TestTaskResultDto>
{
    public TestTaskResultDto Map(TestTaskResult from)
        => new()
        {
            CorrectAnswersCount = from.CorrectAnswersCount,
            ResultInPercents = from.ResultInPercents,
            TotalQuestionsCount = from.TotalQuestionsCount
        };

    public TestTaskResult MapReverse(TestTaskResult dest, TestTaskResultDto src)
    {
        return null!;
    }
}