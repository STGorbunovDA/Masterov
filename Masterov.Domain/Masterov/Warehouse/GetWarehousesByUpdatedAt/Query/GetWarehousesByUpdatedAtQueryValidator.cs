using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt.Query;

public class GetWarehousesByUpdatedAtQueryValidator : AbstractValidator<GetWarehousesByUpdatedAtQuery>
{
    public GetWarehousesByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}