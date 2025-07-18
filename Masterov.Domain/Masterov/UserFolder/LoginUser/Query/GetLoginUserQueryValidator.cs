using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.LoginUser.Query;

public class GetLoginUserQueryValidator : AbstractValidator<GetLoginUserQuery>
{
    public GetLoginUserQueryValidator()
    {
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The login should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The password should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 50");
    }
}