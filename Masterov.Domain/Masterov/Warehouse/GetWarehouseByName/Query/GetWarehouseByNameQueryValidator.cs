using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseByName.Query;

public class GetWarehouseByNameQueryValidator : AbstractValidator<GetWarehouseByNameQuery>
{
    public GetWarehouseByNameQueryValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The Name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the Name should not be more than 100");
    }
}