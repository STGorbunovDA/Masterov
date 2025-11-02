using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByUpdatedAt.Query;

public class GetSuppliersByUpdatedAtQueryValidator : AbstractValidator<GetSuppliersByUpdatedAtQuery>
{
    public GetSuppliersByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}