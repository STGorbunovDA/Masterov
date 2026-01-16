using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByName.Query;

public class GetProductTypesByNameQueryValidator : AbstractValidator<GetProductTypesByNameQuery>
{
    public GetProductTypesByNameQueryValidator()
    {
        RuleFor(c => c.ProductTypeName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The ProductTypeName should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the ProductTypeName should not be more than 100");
    }
}