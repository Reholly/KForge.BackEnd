using Application.DTO.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class LogInDtoValidator : AbstractValidator<LogInDto>
{
    public LogInDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull();
    }
}