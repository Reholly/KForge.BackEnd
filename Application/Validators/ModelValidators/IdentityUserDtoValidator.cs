using Application.DTO.Auth;
using FluentValidation;

namespace Application.Validators.ModelValidators;

public class IdentityUserDtoValidator : AbstractValidator<IdentityUserDto>
{
    public IdentityUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
        
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)   
            .MaximumLength(30);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(5)   
            .MaximumLength(100);
        
        RuleFor(x => x.RepeatedPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(5)   
            .MaximumLength(100)
            .Equal(x => x.Password);
    }
}