using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypeByName.Query;

public class GetProductTypeByNameQueryValidator : AbstractValidator<GetProductTypeByNameQuery>
{
    public GetProductTypeByNameQueryValidator()
    {
        RuleFor(c => c.ProductTypeName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The productTypeName should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the productTypeName should not be more than 100");
    }
}