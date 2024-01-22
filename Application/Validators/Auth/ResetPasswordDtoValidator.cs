using Application.DTO.Security;
using FluentValidation;

namespace Application.Validators.Auth;

public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
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