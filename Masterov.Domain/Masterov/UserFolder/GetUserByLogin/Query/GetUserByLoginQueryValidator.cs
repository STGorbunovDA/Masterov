using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.GetUserByLogin.Query;

public class GetUserByLoginQueryValidator : AbstractValidator<GetUserByLoginQuery>
{
    public GetUserByLoginQueryValidator()
    {
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The login should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
    }
}