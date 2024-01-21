using Application.Models;
using FluentValidation;

namespace Application.Validators.ModelValidators;

public class UserModelValidator : AbstractValidator<ApplicationUserDto>
{
    public UserModelValidator()
    {
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

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .NotNull();
    }
}