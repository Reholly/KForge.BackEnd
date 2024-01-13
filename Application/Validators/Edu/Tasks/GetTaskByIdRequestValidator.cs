using Application.Requests.Education.Tasks;
using FluentValidation;

namespace Application.Validators.Edu.Tasks;

public class GetTaskByIdRequestValidator : AbstractValidator<GetTaskByIdRequest>
{
    public GetTaskByIdRequestValidator()
    {
        RuleFor(request => request.TaskId).NotNull().NotEmpty();
        RuleFor(request => request.Username).NotNull().NotEmpty();
    }
}