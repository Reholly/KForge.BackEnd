using Application.Requests.Auth;
using FluentValidation;

namespace Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(5)   
            .MaximumLength(100);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)   
            .MaximumLength(30);
        
        RuleFor(x => x.Surname)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)   
            .MaximumLength(30);
        
        RuleFor(x => x.Patronymic)
            .NotNull()
            .MaximumLength(30);
    }
}