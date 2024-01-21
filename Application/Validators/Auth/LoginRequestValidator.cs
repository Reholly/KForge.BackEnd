using Application.DTO.Auth;
using Application.Requests.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class LoginRequestValidator : AbstractValidator<LogInDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull();
    }
}