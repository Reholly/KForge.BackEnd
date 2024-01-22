using Application.Requests.Profile;
using FluentValidation;

namespace Application.Validators.Auth;

public class GetProfileRequestValidator : AbstractValidator<GetProfileRequest>
{
    public GetProfileRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull();
    }
}