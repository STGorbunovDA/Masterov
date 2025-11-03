using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt.Query;

public class GetWarehousesByCreatedAtQueryValidator : AbstractValidator<GetWarehousesByCreatedAtQuery>
{
    public GetWarehousesByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}