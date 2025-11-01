using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName.Query;

public class GetComponentTypesByNameQueryValidator : AbstractValidator<GetComponentTypesByNameQuery>
{
    public GetComponentTypesByNameQueryValidator()
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