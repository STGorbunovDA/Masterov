using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.GetUserByLogin.Query;

public class GetUserByLoginQueryValidator : AbstractValidator<GetUserByLoginQuery>
{
    public GetUserByLoginQueryValidator()
    {
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithErrorCode("Required")
                .WithMessage("Login is required.")
            .MaximumLength(100)
                .WithErrorCode("TooLong")
                .WithMessage("The maximum length of the login should not be more than 100");
    }
}