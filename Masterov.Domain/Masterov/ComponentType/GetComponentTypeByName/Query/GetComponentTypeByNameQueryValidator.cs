using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName.Query;

public class GetComponentTypeByNameQueryValidator : AbstractValidator<GetComponentTypeByNameQuery>
{
    public GetComponentTypeByNameQueryValidator()
    {
        RuleFor(c => c.ComponentTypeName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The componentTypeName should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the componentTypeName should not be more than 100");
    }
}