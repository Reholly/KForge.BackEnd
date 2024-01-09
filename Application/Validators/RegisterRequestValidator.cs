using Application.Models;
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
        
        RuleFor(x => x.ApplicationUserModel.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)   
            .MaximumLength(30);
        
        RuleFor(x => x.ApplicationUserModel.Surname)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)   
            .MaximumLength(30);
                
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)   
            .MaximumLength(30);
        
        RuleFor(x => x.ApplicationUserModel.Patronymic)
            .NotNull()
            .MaximumLength(30);
    }
}