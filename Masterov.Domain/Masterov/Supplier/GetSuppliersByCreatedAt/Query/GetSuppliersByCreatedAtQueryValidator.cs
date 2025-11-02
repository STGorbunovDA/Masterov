using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByCreatedAt.Query;

public class GetSuppliersByCreatedAtQueryValidator : AbstractValidator<GetSuppliersByCreatedAtQuery>
{
    public GetSuppliersByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}