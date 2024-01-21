using Application.DTO.Security;
using Application.Requests.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordRequestValidator()
    { 
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.RepeatedNewPassword)
            .NotEmpty()
            .NotNull()
            .Equal(x => x.NewPassword);
    }
}